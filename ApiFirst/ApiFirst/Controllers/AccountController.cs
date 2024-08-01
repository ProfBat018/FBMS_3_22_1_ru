using ApiFirst.Data.Models.Requests;
using ApiFirst.Exceptions;
using ApiFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirst.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [Authorize]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetRequest)
        {
            try
            {
                await accountService.ResetPaswordAsync(resetRequest);
                return Ok("Recovery link sent to your email");
            }
            catch (MyAuthException ex)
            {
                return BadRequest($"{ex.Message}\n{ex.AuthErrorType}");
            }
        }

    }
}
