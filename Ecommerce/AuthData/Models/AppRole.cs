namespace AuthData.Models;

public  class AppRole
{
    public Guid RoleId { get; set; } = Guid.NewGuid();
    public string RoleName { get; set; }
}