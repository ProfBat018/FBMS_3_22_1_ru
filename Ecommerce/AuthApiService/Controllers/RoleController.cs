using AuthData.DTO;
using Microsoft.AspNetCore.Mvc;
using UserService.Classes;
using UserService.Interfaces;

namespace AuthApiService.Controllers;

[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    
    [HttpGet("Roles/All")]
    public async Task<IActionResult> GetRolesAsync()
    {
        return Ok(await _roleService.GetRolesAsync());
    }
    
    [HttpPost("Roles/Add")]
    public async Task AddRoleAsync([FromBody] RoleDTO role)
    {
        _roleService.AddNewRoleAsync(role);
    }
}