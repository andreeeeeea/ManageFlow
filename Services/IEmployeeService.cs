using EmployeeManager.Data.Models;

namespace EmployeeManager.Services;

public interface IEmployeeService
{
    Task<List<Employees>> GetEmployeesAsync();
    Task<Employees> CreateEmployeeAsync(Employees employee);
    Task<Employees> UpdateEmployeeAsync(Employees employee);
    Task DeleteEmployeeAsync(int employeeId);
}