using ManageFlow.Data.Models;

namespace ManageFlow.Services;

public interface ITaskManagerService
{
    Task<List<Employees>> GetEmployeesAsync();
    Task<Tasks> CreateTaskAsync(Tasks task);
    Task AssignEmployeesToTaskAsync(int taskId, List<int> employeeIds);
    Task<List<Tasks>> GetTasksAsync();
    Task<List<Tasks>> GetLatestTasksAsync();
    Task<List<Tasks>> GetCompletedTasksAsync();
    Task<Tasks> UpdateTaskAsync(Tasks task);
    Task DeleteTaskAsync(int taskId);
    Task<List<TaskAssignments>> GetTaskAssignmentsAsync(int taskId);
}