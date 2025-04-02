using EmployeeManager.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManager.Services;

public interface IAuthService 
{
    Task<SignInResult> LoginAsync(LoginModel loginModel);
    Task LogoutAsync();
    Task<bool> IsAuthenticatedAsync();
}