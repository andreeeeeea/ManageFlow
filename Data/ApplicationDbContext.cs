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
        public DbSet<Departments> Departments { get; set; } 

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

            builder.Entity<Departments>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Departments>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Departments>()
                .HasMany(d => d.Tasks)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        
    }
}
