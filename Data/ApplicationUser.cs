using Microsoft.AspNetCore.Identity;
using ManageFlow.Data.Models;

namespace ManageFlow.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

}

