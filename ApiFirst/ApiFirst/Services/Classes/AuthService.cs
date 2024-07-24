using ApiFirst.Data.Contexts;
using ApiFirst.Data.Models;
using ApiFirst.Services.Interfaces;

using static BCrypt.Net.BCrypt;

namespace ApiFirst.Services.Classes;

public class AuthService : IAuthService
{
    private readonly AuthContext context;
    public AuthService(AuthContext context)
    {
        this.context = context;
    }

    public async Task<User> LoginUserAsync(LoginUser user)
    {

        throw new NotImplementedException();
    }

    public async Task<User> RegisterUserAsync(RegisterUser user)
    {
        try
        {
            var newUser = new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = HashPassword(user.Password)
            };

            await context.AddAsync(newUser);
            await context.SaveChangesAsync();

            return newUser;
        }
        catch
        {
            throw;
        }
    }
}
