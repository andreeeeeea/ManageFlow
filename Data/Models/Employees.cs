using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ManageFlow.Data.Models;

[Table("employees")]
public class Employees : BaseModel {
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;

    [Column("department")]
    public string Department { get; set; } = string.Empty;

    [Column("position")]
    public string Position { get; set; } = string.Empty;

    [Column("salary")]
    public decimal Salary { get; set; }
}