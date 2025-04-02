using Microsoft.AspNetCore.Identity;
using EmployeeManager.Data.Models;

namespace EmployeeManager.Data.Stores;

public class SupabaseUserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>
{
    private readonly Supabase.Client _supabase;

    public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.NormalizedUserName);
    }

    public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
        return Task.CompletedTask;
    }

    public SupabaseUserStore(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(user.Id))
            {
                user.Id = Guid.NewGuid().ToString();
            }

            var response = await _supabase.From<ApplicationUser>().Insert(user);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        try
        {
            if (!Guid.TryParse(userId, out _))
            {
                return null;
            }

            var response = await _supabase.From<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Get();    
            return response?.Models.FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error finding user by ID: {e.Message}");
            return null;
        }
    }

    public async Task<ApplicationUser?> FindByNameAsync(string username, CancellationToken cancellationToken)
    {
        try 
        {
            var response = await _supabase.From<ApplicationUser>().Where(u => u.UserName == username).Get();
            return response?.Models.FirstOrDefault();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error finding user by username: {e.Message}");
            return null;
        }
    }

    public Task<string?> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public Task SetUserNameAsync(ApplicationUser user, string? username, CancellationToken cancellationToken)
    {
        user.UserName = username;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        try
        {
            await _supabase.From<ApplicationUser>().Where(u => u.Id == user.Id).Update(user);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        try
        {
            await _supabase.From<ApplicationUser>().Where(u => u.Id == user.Id).Delete();
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
    }

    public void Dispose()
    {
    }
}