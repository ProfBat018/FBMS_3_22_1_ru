using ApiFirst.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiFirst.Data.Models
{
    public class UserStore : IUserStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IRoleStore<User>
    {

        private readonly AuthContext context;

        public UserStore(AuthContext context)
        {
            this.context = context;
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            context.Users.Add(user);
            return context.SaveChangesAsync(cancellationToken).ContinueWith(t =>
                t.IsFaulted ? IdentityResult.Failed(new IdentityError { Description = "Failed to create user" }) : IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            context.Users.Remove(user);
            return context.SaveChangesAsync(cancellationToken).ContinueWith(t =>
                t.IsFaulted ? IdentityResult.Failed(new IdentityError { Description = "Failed to delete user" }) : IdentityResult.Success);
        }

        public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId, cancellationToken);
        }

        public Task<User> FindByNameAsync(string userName, CancellationToken cancellationToken)
        {
            return context.Users.SingleOrDefaultAsync(u => u.Username == userName, cancellationToken);
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username.ToUpperInvariant());
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email.ToUpperInvariant());
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            // No-op for custom implementation
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // No-op for custom implementation
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.CompletedTask;
        }

        public Task<bool> VerifyUserPasswordAsync(User user, string password, CancellationToken cancellationToken)
        {
            // Implement password verification (e.g., compare hashed password)
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, user.Password));
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            context.Users.Update(user);
            return context.SaveChangesAsync(cancellationToken).ContinueWith(t =>
                           t.IsFaulted ? IdentityResult.Failed(new IdentityError { Description = "Failed to update user" }) : IdentityResult.Success);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(User role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetRoleNameAsync(User role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(User role, string? roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetNormalizedRoleNameAsync(User role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(User role, string? normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
