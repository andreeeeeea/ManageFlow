namespace ManageFlow.Data.Models
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual ApplicationUser User { get; set;  } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}