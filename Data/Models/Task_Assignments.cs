using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace EmployeeManager.Data.Models;

[Table("task_assignments")]
public class TaskAssignments : BaseModel {
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("task_id")]
    public int TaskId { get; set; }
} 