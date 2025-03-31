using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("task_assignments")]
public class TaskAssignments : BaseModel {
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
} 