using ApiFirst.Data.Models;
using ApiFirst.Data.Models.Requests;
using ApiFirst.Exceptions;
using ApiFirst.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ApiFirst.Services.Classes;

public class AccountService : IAccountService
{
    private readonly UserManager<User> userManager;
    private readonly IEmailSender emailSender;
    private readonly ITokenService tokenService;

    public AccountService(IEmailSender emailSender, ITokenService tokenService, UserManager<User> userManager)
    {
        this.emailSender = emailSender;
        this.tokenService = tokenService;
        this.userManager = userManager;
    }

    public async Task ResetPaswordAsync(ResetPasswordRequest resetRequest)
    {
        var principal = tokenService.GetPrincipalFromToken(resetRequest.Token, validateLifetime: true);

        var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var user = await userManager.FindByNameAsync(username);

        if (user == null)
        {
            throw new MyAuthException(AuthErrorTypes.UserNotFound, "User not found");
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        await userManager.ResetPasswordAsync(user, token, resetRequest.NewPassword);

        await emailSender.SendEmailAsync(user.Email, "Password reset", "Your password has been reset");
    }


}
