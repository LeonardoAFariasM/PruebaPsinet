using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces;

public interface IRoverService
{
    Task AddTaskAsync(RoverTask task);
    Task<List<RoverTask>> GetDailyTasksAsync(string roverName, DateTime date);
    Task<double> CalculateUtilizationAsync(string roverName, DateTime date);
    Task<IEnumerable<string>> GetRoversAsync();
    Task<List<RoverTask>> GetAllDailyTasksAsync(DateTime date);
}

