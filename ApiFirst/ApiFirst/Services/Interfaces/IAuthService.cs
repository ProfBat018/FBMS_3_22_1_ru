using ApiFirst.Data.Models;
using ApiFirst.Data.Models.Requests;

namespace ApiFirst.Services.Interfaces;

public interface IAuthService
{
    public Task<TokenData> LoginUserAsync(LoginUser user);
    public Task<User> RegisterUserAsync(RegisterUser user);
    public Task<TokenData> RefreshTokenAsync(UserTokenInfo userAccessData);

    public Task LogOutAsync(UserTokenInfo userTokenInfo);


}
