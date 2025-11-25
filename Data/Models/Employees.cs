namespace ManageFlow.Data.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public virtual Departments? Department { get; set;}
        public string Position { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<TaskAssignments> TaskAssignments { get; set; } = new List<TaskAssignments>();
    }
}
