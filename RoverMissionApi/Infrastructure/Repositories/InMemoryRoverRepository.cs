using System.Collections.Concurrent;
using Domain.Interfaces;
using Domain.Entities; 

namespace Infrastructure.Repositories;
// Configuracion del repositorio en memoria
public class InMemoryRoverRepository : IRoverRepository
{
    private readonly ConcurrentDictionary<string, List<RoverTask>> _data = new();

    public Task AddTaskAsync(RoverTask task)
    {
        _data.AddOrUpdate(task.RoverName,
            new List<RoverTask> { task },
            (_, tasks) =>
            {
                tasks.Add(task);
                return tasks;
            });
        return Task.CompletedTask;
    }

    public Task<List<RoverTask>> GetDailyTasksAsync(string roverName, DateTime date)
    {
        var tasks = _data.TryGetValue(roverName, out var roverTasks)
            ? roverTasks.Where(t => t.StartsAt.Date == date.Date)
                       .OrderBy(t => t.StartsAt)
                       .ToList()
            : new List<RoverTask>();
        return Task.FromResult(tasks);
    }

    public Task<IEnumerable<string>> GetAllRoverNamesAsync()
    {
        
        var roverNames = _data.Keys;
        return Task.FromResult(roverNames.AsEnumerable());
    }

}