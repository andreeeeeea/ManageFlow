using Microsoft.EntityFrameworkCore;
using ManageFlow.Data.Models;
using ManageFlow.Data;

namespace ManageFlow.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _context;

        public DepartmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departments>> GetDepartmentsAsync()
        {
            return await _context.Departments
                .Include(d => d.Manager)
                .ToListAsync();
        }

        public async Task<Departments> CreateDepartmentAsync(Departments department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Departments> UpdateDepartmentAsync(Departments department)
        {
            var existingDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Id == department.Id);

            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                existingDepartment.Description = department.Description;
                existingDepartment.ManagerId = department.ManagerId;

                await _context.SaveChangesAsync();
                return existingDepartment;
            }

            return null!;
        }

        public async Task<Departments>? GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<Employees?> GetDepartmentManagerAsync(int departmentId)
        {
            var department = await _context.Departments
                .Include(d => d.Manager)
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            return department?.Manager;
        }

        public async Task<List<Employees>> GetDepartmentEmployeesAsync(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<List<Tasks>> GetDepartmentTasksAsync(int departmentId)
        {
            return await _context.Tasks
                .Where(t => t.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<List<Tasks>> GetDepartmentTasksByStatusAsync(int departmentId, string status)
        {
            return await _context.Tasks
                .Where(t => t.DepartmentId == departmentId && t.Status == status)
                .ToListAsync();
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}