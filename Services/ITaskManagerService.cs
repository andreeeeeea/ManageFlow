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
    Task<List<Tasks>> GetInProgressTasksAsync();
    Task<List<Tasks>> GetNotStartedTasksAsync();
    Task<List<Tasks>> GetTodayTasksAsync();
    Task<List<Tasks>> GetTomorrowTasksAsync();
    Task<List<Tasks>> GetPreviousWeekTasksAsync();
    Task<List<Tasks>> GetOverdueTasksAsync();
    Task<Tasks> UpdateTaskAsync(Tasks task);
    Task<Tasks> GetTaskByIdAsync(int taskId);
    Task DeleteTaskAsync(int taskId);
    Task<List<TaskAssignments>> GetTaskAssignmentsAsync(int taskId);
}