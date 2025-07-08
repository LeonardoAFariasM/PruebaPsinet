using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class RoverService : IRoverService
    {
        private readonly IRoverRepository _repository;

        public RoverService(IRoverRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task AddTaskAsync(RoverTask task)
        {
            var existingTasks = await _repository.GetDailyTasksAsync(task.RoverName, task.StartsAt.Date);
            var newTaskEnd = task.StartsAt.AddMinutes(task.DurationMinutes);

            foreach (var existing in existingTasks)
            {
                var existingEnd = existing.StartsAt.AddMinutes(existing.DurationMinutes);
                if (task.StartsAt < existingEnd && newTaskEnd > existing.StartsAt)
                    throw new TaskConflictException("Task overlaps with existing schedule");
            }
            await _repository.AddTaskAsync(task);
        }

        public Task<List<RoverTask>> GetDailyTasksAsync(string roverName, DateTime date)
        {
            return _repository.GetDailyTasksAsync(roverName, date);
        }

        public async Task<double> CalculateUtilizationAsync(string roverName, DateTime date)
        {
            var tasks = await _repository.GetDailyTasksAsync(roverName, date);
            return Math.Min(100, tasks.Sum(t => t.DurationMinutes) / 14.4);
        }

        public async Task<IEnumerable<string>> GetRoversAsync()
        {
            return await _repository.GetAllRoverNamesAsync();
        }
        
        public async Task<List<RoverTask>> GetAllDailyTasksAsync(DateTime date)
        {
            var roverNames = await _repository.GetAllRoverNamesAsync();
            var allTasks = new List<RoverTask>();

            foreach (var rover in roverNames)
            {
                var tasks = await _repository.GetDailyTasksAsync(rover, date);
                allTasks.AddRange(tasks);
            }

            return allTasks;
        }
    }
}
