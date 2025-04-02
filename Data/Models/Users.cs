using Microsoft.AspNetCore.Identity;
using Supabase.Postgrest.Attributes;

namespace EmployeeManager.Data.Models;

[Table("users")]
public class ApplicationUser : IdentityUser
{
    [Column("first_name")]
    public string FirstName { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("role")]
    public string Role { get; set; }
}