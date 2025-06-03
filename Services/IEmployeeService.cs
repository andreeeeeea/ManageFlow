using ManageFlow.Data.Models;

namespace ManageFlow.Services;

public interface IEmployeeService
{
    Task<List<Employees>> GetEmployeesAsync();
    Task<Dictionary<string, int>> GetEmployeeRolesCountAsync();
    Task<Employees> CreateEmployeeAsync(Employees employee);
    Task<Employees> UpdateEmployeeAsync(Employees employee);

    Task<Employees>? GetEmployeeByIdAsync(int employeeId);
    Task DeleteEmployeeAsync(int employeeId);
}