using System.Data.Common;
using AuthData.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthData.Contexts;

public class AuthContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public  DbSet<AppRole> AppRoles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    public AuthContext()
    {
        
    }

    public AuthContext(DbContextOptions<AuthContext> options): base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var userEntity = modelBuilder.Entity<User>();
        var appRoleEntity = modelBuilder.Entity<AppRole>();
        var userRoleEntity = modelBuilder.Entity<UserRole>();

        userEntity.HasKey(u => u.Id); // primary key

        userEntity.Property(u => u.Username)
            .IsRequired();
        userEntity.HasIndex(u => u.Username)
            .IsUnique();

        userEntity.Property(u => u.Email)
            .IsRequired();
        userEntity.HasIndex(u => u.Email)
            .IsUnique();

        userEntity.Property(u => u.Password)
            .IsRequired();

        appRoleEntity.HasKey(a => a.RoleId);
        
        appRoleEntity.Property(a => a.RoleName)
            .HasMaxLength(15)
            .IsRequired();

        appRoleEntity.HasIndex(a => a.RoleName)
            .IsUnique();
            
    }



}
