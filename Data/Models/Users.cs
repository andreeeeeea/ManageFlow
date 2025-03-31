using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("users")]
public class Users : BaseModel 
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("username")]
    public string Username { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("role")]
    public string Role { get; set; } = string.Empty;
}