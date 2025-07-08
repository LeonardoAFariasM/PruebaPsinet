// Interfaz llamada en InMemoryRoverRepository
using Domain.Interfaces;
using Domain.Entities;
namespace Domain.Interfaces;

public interface IRoverRepository
{
    Task AddTaskAsync(RoverTask task);
    Task<List<RoverTask>> GetDailyTasksAsync(string roverName, DateTime date);
    Task<IEnumerable<string>> GetAllRoverNamesAsync();
}
public class TaskConflictException : Exception
{
    public TaskConflictException(string message) : base(message) { }
}