using EmployeeManager.Data.Models;

namespace EmployeeManager.Services;

public class TaskManagerService : ITaskManagerService
{
    private readonly Supabase.Client _supabase;

    public TaskManagerService(Supabase.Client supabase)
    {
        _supabase = supabase;
    }

    public async Task<List<Employees>> GetEmployeesAsync()
    {
        var response = await _supabase.From<Employees>().Select("id, first_name, last_name, department").Get();
        return response?.Models ?? new List<Employees>();
    }

    public async Task<Tasks> CreateTaskAsync(Tasks task)
    {
        var response = await _supabase.From<Tasks>().Insert(task);
        return response.Models.FirstOrDefault();
    }

    public async Task AssignEmployeesToTaskAsync(int taskId, List<int> employeeIds)
    {
        await _supabase.From<TaskAssignments>().Where(x => x.TaskId == taskId).Delete();
        foreach (var employeeId in employeeIds)
        {
            await _supabase.From<TaskAssignments>().Insert(new TaskAssignments
            {
                TaskId = taskId,
                EmployeeId = employeeId
            });
        }
    }

    public async Task<List<Tasks>> GetTasksAsync()
    {
        var response = await _supabase.From<Tasks>().Get();
        return response?.Models ?? new List<Tasks>();
    }

    public async Task<Tasks> UpdateTaskAsync(Tasks task)
    {
        var response = await _supabase.From<Tasks>().Where(x => x.Id == task.Id).Update(task);
        return response.Models.FirstOrDefault();
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        await _supabase.From<Tasks>().Where(x => x.Id == taskId).Delete();
    }

    public async Task<List<TaskAssignments>> GetTaskAssignmentsAsync(int taskId)
    {
        var response = await _supabase.From<TaskAssignments>().Where(x => x.TaskId == taskId).Get();
        return response?.Models ?? new List<TaskAssignments>();
    }
}