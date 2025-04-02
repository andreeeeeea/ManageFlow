using EmployeeManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Supabase;

namespace EmployeeManager.Data.Stores;

public class SupabaseRoleStore : IRoleStore<IdentityRole>
{
    private readonly Client _supabase;
 
    public SupabaseRoleStore(Client supabase)
    {
        _supabase = supabase;
    }

public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
{
    try
    {
        var userRole = new UserRole
        {
            RoleId = role.Id,
            NormalizedUsername = role.NormalizedName
        };
            
        await _supabase.From<UserRole>().Insert(userRole);
        return IdentityResult.Success;
    }
    catch (Exception e)
    {
        return IdentityResult.Failed(new IdentityError { Description = e.Message });
    }
}

    public async Task<IdentityRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        try 
        {
            var response = await _supabase.From<UserRole>().Where(u => u.RoleId == roleId).Get();
            var userRole = response?.Models.FirstOrDefault();
            return userRole == null ? null : new IdentityRole
            {
                Id = userRole.RoleId,
                NormalizedName = userRole.NormalizedUsername
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error finding role by ID: {e.Message}");
            return null;
        }
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id);
    }

    public Task<string?> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(IdentityRole role, string? roleName, CancellationToken cancellationToken)
    {
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        try
        {
            var userRole = new UserRole
            {
                RoleId = role.Id,
                NormalizedUsername = role.NormalizedName
            };

            await _supabase.From<UserRole>().Where(u => u.RoleId == role.Id).Update(userRole);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        try
        {
            await _supabase.From<UserRole>().Where(u => u.RoleId == role.Id).Delete();
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Description = e.Message });
        }
    }

    public async Task<IdentityRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _supabase.From<UserRole>().Where(u => u.NormalizedUsername == normalizedRoleName).Get();
            var userRole = response?.Models.FirstOrDefault();
            return userRole == null ? null : new IdentityRole
            {
                Id = userRole.RoleId,
                NormalizedName = userRole.NormalizedUsername
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error finding role by name: {e.Message}");
            return null;
        }
    }


    public void Dispose()
    {
    }
}