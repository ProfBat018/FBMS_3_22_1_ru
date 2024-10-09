namespace AuthData.Models;

public class UserRole
{
    public Guid Id { get; set; } = new Guid();
    public string UserId { get; set; }
    public string RoleId { get; set; }
    
}