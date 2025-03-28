using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("tasks")]
public class Tasks : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Column("status")]
    public string Status { get; set; } = string.Empty;

    [Column("deadline")]
    public DateTime? Deadline { get; set; }
}