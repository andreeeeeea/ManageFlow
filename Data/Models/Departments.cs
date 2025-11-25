namespace ManageFlow.Data.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Employees> Employees { get; set; } = new List<Employees>();
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

        public int? ManagerId { get; set; }
        public virtual Employees? Manager { get; set; }
    }
}