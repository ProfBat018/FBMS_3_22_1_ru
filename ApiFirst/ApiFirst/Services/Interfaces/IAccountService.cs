using ApiFirst.Data.Models.Requests;

namespace ApiFirst.Services.Interfaces;

public interface IAccountService
{
    public Task ResetPaswordAsync(ResetPasswordRequest resetRequest);
}
