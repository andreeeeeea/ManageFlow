using System;

namespace ManageFlow.Data.Models
{
    public class TaskAssignments
    {
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }

        public Tasks Task { get; set; }
        public Employees Employee { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
