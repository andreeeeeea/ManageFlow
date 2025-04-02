using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("user_roles")]
public class UserRole : BaseModel
{
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("role_id")]
    public string RoleId { get; set; } = string.Empty;

    [Column("normalized_username")]
    public string NormalizedUsername { get; set; } = string.Empty;
}