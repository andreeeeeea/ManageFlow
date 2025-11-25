using ManageFlow.Data.Models;

namespace ManageFlow.Services;

public interface IDepartmentService
{
    Task<List<Departments>> GetDepartmentsAsync();
    Task<Departments> CreateDepartmentAsync(Departments department);
    Task<Departments> UpdateDepartmentAsync(Departments department);
    Task<Departments>? GetDepartmentByIdAsync(int departmentId);
    Task<Employees?> GetDepartmentManagerAsync(int departmentId);
    Task<List<Employees>> GetDepartmentEmployeesAsync(int departmentId);
    Task<List<Tasks>> GetDepartmentTasksAsync(int departmentId);
    Task<List<Tasks>> GetDepartmentTasksByStatusAsync(int departmentId, string status);
    Task DeleteDepartmentAsync(int departmentId);
}