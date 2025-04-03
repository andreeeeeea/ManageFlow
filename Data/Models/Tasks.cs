using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace ManageFlow.Data.Models;

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

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}