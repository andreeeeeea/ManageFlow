namespace ManageFlow.Data.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public WorkStatus Status { get; set; } = WorkStatus.ToDo;
        public DateTime? Deadline { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Departments? Department { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<TaskAssignments> TaskAssignments { get; set; } = new List<TaskAssignments>();
    }
}
