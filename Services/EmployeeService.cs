using ManageFlow.Data.Models;

namespace ManageFlow.Services;

public class EmployeeService : IEmployeeService
{
    private readonly Supabase.Client _supabase;

    public EmployeeService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<List<Employees>> GetEmployeesAsync()
    {
        var response = await _supabase.From<Employees>().Get();
        return response?.Models ?? new List<Employees>();
    }

    public async Task<Employees> CreateEmployeeAsync(Employees employee)
    {
        var response = await _supabase.From<Employees>().Insert(employee);
        return response.Models.FirstOrDefault();
    }

    public async Task<Employees> UpdateEmployeeAsync(Employees employee)
    {
        var response = await _supabase.From<Employees>().Where(x => x.Id == employee.Id).Update(employee);
        return response.Models.FirstOrDefault();
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        await _supabase.From<Employees>().Where(x => x.Id == employeeId).Delete();
    }
}