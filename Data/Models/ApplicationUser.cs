using Microsoft.AspNetCore.Identity;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("users")]
public class ApplicationUser : BaseModel
{
    [PrimaryKey("id")]
    [Column("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("username")]
    public string UserName { get; set; } = string.Empty;

    [Column("normalized_username")]
    public string NormalizedUserName { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("normalized_email")]
    public string NormalizedEmail { get; set; } = string.Empty;

    [Column("email_confirmed")]
    public bool EmailConfirmed { get; set; }

    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;

    [Column("role")]
    public string Role { get; set; } = "Employee";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}