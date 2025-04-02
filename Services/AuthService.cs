using EmployeeManager.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManager.Services;

public class AuthService : IAuthService 
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> LoginAsync(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        return await _signInManager.PasswordSignInAsync(
            user.UserName ?? string.Empty,
            loginModel.Password,
            isPersistent: true,
            lockoutOnFailure: false
        );
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        return (await _signInManager.Context.AuthenticateAsync()).Succeeded;
    }
}