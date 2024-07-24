using ApiFirst.Data.Contexts;
using ApiFirst.Data.Models;
using ApiFirst.Services.Classes;
using ApiFirst.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirst.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUserValidator loginValidator;
    private readonly RegisterUserValidator registerValidator;
    private readonly AuthService authService;


    public AuthController(LoginUserValidator loginValidator, RegisterUserValidator registerValidator, AuthService authService)
    {
        this.loginValidator = loginValidator;
        this.registerValidator = registerValidator;
        this.authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUser user)
    {
        return Ok();
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUser user)
    {
        try
        {
            var validationResult = registerValidator.Validate(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var res = await authService.RegisterUserAsync(user);
            return Ok(res);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
