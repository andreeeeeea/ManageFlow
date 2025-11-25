using Microsoft.EntityFrameworkCore;
using ManageFlow.Data.Models;
using ManageFlow.Data;

namespace ManageFlow.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employees>> GetEmployeesAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetEmployeeRolesCountAsync()
        {
            return await _context.Employees
                .Where(e => e.Department != null)
                .GroupBy(e => e.Department!.Name)
                .ToDictionaryAsync(g => g.Key, g => g.Count());
        }
        

        public async Task<Employees> CreateEmployeeAsync(Employees employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task<Employees> UpdateEmployeeAsync(Employees employee)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (existingEmployee != null)
            {
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.DepartmentId = employee.DepartmentId;                
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;

                await _context.SaveChangesAsync();
                return existingEmployee;
            }

            return null;
        }

        public async Task<Employees>? GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
