using AuthData.DTO;

namespace UserService.Interfaces;

public interface IRoleService
{
    public Task AddNewRoleAsync(RoleDTO role);
    public Task<List<RoleDTO>> GetRolesAsync();
}