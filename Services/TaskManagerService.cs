using ManageFlow.Data.Models;
using Microsoft.EntityFrameworkCore;
using ManageFlow.Data;

namespace ManageFlow.Services;

public class TaskManagerService : ITaskManagerService
{
    private readonly ApplicationDbContext _context;

    public TaskManagerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employees>> GetEmployeesAsync()
    {
        return await _context.Employees
            .Select(e => new Employees
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Department = e.Department
            })
            .ToListAsync();
    }

    public async Task<Tasks> CreateTaskAsync(Tasks task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task AssignEmployeesToTaskAsync(int taskId, List<int> employeeIds)
    {
        var existingAssignments = await _context.TaskAssignments.Where(x => x.TaskId == taskId).ToListAsync();

        _context.TaskAssignments.RemoveRange(existingAssignments);
        
        foreach (var employeeId in employeeIds)
        {
            var taskAssignment = new TaskAssignments
            {
                TaskId = taskId,
                EmployeeId = employeeId
            };
            _context.TaskAssignments.Add(taskAssignment);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<List<Tasks>> GetTasksAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<Tasks> UpdateTaskAsync(Tasks task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TaskAssignments>> GetTaskAssignmentsAsync(int taskId)
    {
        return await _context.TaskAssignments.Where(x => x.TaskId == taskId).ToListAsync();
    }
}
