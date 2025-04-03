using ManageFlow.Data.Models;

namespace ManageFlow.Services;

public interface IEmployeeService
{
    Task<List<Employees>> GetEmployeesAsync();
    Task<Employees> CreateEmployeeAsync(Employees employee);
    Task<Employees> UpdateEmployeeAsync(Employees employee);
    Task DeleteEmployeeAsync(int employeeId);
}