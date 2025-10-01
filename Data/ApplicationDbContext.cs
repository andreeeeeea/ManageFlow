using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ManageFlow.Data.Models;

namespace ManageFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for each model
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskAssignments> TaskAssignments { get; set; }
         public DbSet<Role> CustomRoles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> CustomUserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TaskAssignments>()
                .HasKey(ta => new { ta.TaskId, ta.EmployeeId });

            builder.Entity<TaskAssignments>()
                .HasOne(ta => ta.Task)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(ta => ta.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskAssignments>()
                .HasOne(ta => ta.Employee)
                .WithMany(e => e.TaskAssignments)
                .HasForeignKey(ta => ta.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Employees>()
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employees>(e => e.UserId)
                .IsRequired(false);

            builder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

        }
        
    }
}
