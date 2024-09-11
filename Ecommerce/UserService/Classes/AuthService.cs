﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AuthData.Contexts;
using AuthData.DTO;
using AuthData.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using UserService.Exceptions;
using UserService.Interfaces;
using static BCrypt.Net.BCrypt;

namespace UserService.Classes;


public class AuthService : IAuthService
{
    private readonly AuthContext context;
    private readonly ITokenService tokenService;
    private readonly IBlackListService blackListService;
    public AuthService(AuthContext context, ITokenService tokenService, IBlackListService blackListService, IEmailSender emailSender)
    {
        this.context = context;
        this.tokenService = tokenService;
        this.blackListService = blackListService;
    }

    public async Task<AccessInfoDTO> LoginUserAsync(LoginDTO user)
    {
        try
        {
            var foundUser = await context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);

            if (foundUser == null)
            {
                throw new MyAuthException(AuthErrorTypes.UserNotFound, "User not found");
            }

            if (!Verify(user.Password, foundUser.Password))
            {
                throw new MyAuthException(AuthErrorTypes.InvalidCredentials, "Invalid credentials");
            }

            var tokenData = new AccessInfoDTO(
                await tokenService.GenerateTokenAsync(foundUser),
                await tokenService.GenerateRefreshTokenAsync(),
                DateTime.Now.AddDays(1)
            );

            foundUser.RefreshToken = tokenData.RefreshToken;
            foundUser.RefreshTokenExpiryTime = tokenData.RefreshTokenExpireTime;

            await context.SaveChangesAsync();

            return tokenData;
        }
        catch
        {
            throw;
        }
    }

    public async Task LogOutAsync(TokenDTO userTokenInfo)
    {
        if (userTokenInfo is null)
            throw new MyAuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var principal = tokenService.GetPrincipalFromToken(userTokenInfo.AccessToken);

        var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = context.Users.FirstOrDefault(u => u.Username == username);

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.Now;
        await context.SaveChangesAsync();

        blackListService.AddTokenToBlackList(userTokenInfo.AccessToken);
              
    }

    public async Task<AccessInfoDTO> RefreshTokenAsync(TokenDTO userAccessData)
    {
        if (userAccessData is null)
            throw new MyAuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var accessToken = userAccessData.AccessToken;
        var refreshToken = userAccessData.RefreshToken;

        var principal = tokenService.GetPrincipalFromToken(accessToken);

        var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = context.Users.FirstOrDefault(u => u.Username == username);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new MyAuthException(AuthErrorTypes.InvalidRequest, "Invalid client request");

        var newAccessToken = await tokenService.GenerateTokenAsync(user);
        var newRefreshToken = await tokenService.GenerateRefreshTokenAsync();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);

        await context.SaveChangesAsync();

        return new AccessInfoDTO(
            newAccessToken,
            newRefreshToken,
            user.RefreshTokenExpiryTime);
    }

    public async Task<User> RegisterUserAsync(RegisterDTO user)
    {
        try
        {
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = HashPassword(user.Password)
            };

            await context.Users.AddAsync(newUser);

            await context.SaveChangesAsync();

            return newUser;
        }
        catch
        {
            throw;
        }
    }
}
