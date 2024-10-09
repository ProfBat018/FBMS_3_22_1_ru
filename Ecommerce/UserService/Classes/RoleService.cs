using System.Collections;
using AuthData.Configs;
using AuthData.Contexts;
using AuthData.DTO;
using AuthData.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserService.Interfaces;

namespace UserService.Classes;

public class RoleService : IRoleService
{
    private readonly AuthContext _context;
    private readonly Mapper _mapper;

    public RoleService(AuthContext context)
    {
        _context = context;
        _mapper = MappingConfiguration.InitializeConfig();
    }

    public async Task AddNewRoleAsync(RoleDTO role)
    {
        try
        {
            var mappedRole = _mapper.Map<RoleDTO, AppRole>(role);
            
            await _context.AppRoles.AddAsync(mappedRole);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<RoleDTO>> GetRolesAsync()
    {
        try
        {

            var roles = await _context.AppRoles.ToListAsync();
            return _mapper.Map<List<AppRole>, List<RoleDTO>>(roles);
        }
        catch
        {
            throw;
        }
    }
}