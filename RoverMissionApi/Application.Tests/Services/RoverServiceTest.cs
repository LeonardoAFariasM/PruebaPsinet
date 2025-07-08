// Application.Tests/Services/RoverServiceTests.cs
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using Domain.Interfaces;

namespace Application.Tests.Services;

public class RoverServiceTests
{
    [Fact]
    public async Task AddTask_ShouldThrowConflict_WhenTasksOverlap()
    {
        // Arrange (Preparar)
        var existingTask = new RoverTask
        {
            Id = Guid.NewGuid(),
            RoverName = "Perseverance",
            StartsAt = new DateTime(2025, 1, 1, 10, 0, 0),
            DurationMinutes = 60 // 10:00 - 11:00
        };
        
        var newTask = new RoverTask
        {
            RoverName = "Perseverance",
            StartsAt = new DateTime(2025, 1, 1, 10, 30, 0), // Se superpone
            DurationMinutes = 30 // 10:30 - 11:00
        };

        // Configuramos el repositorio simulado (mock)
        var mockRepo = new Mock<IRoverRepository>();
        mockRepo.Setup(r => r.GetDailyTasksAsync("Perseverance", newTask.StartsAt.Date))
                .ReturnsAsync(new List<RoverTask> { existingTask });
        
        var service = new RoverService(mockRepo.Object);

        // Act (Actuar) & Assert (Afirmar)
        await Assert.ThrowsAsync<Domain.Interfaces.TaskConflictException>(
            () => service.AddTaskAsync(newTask)
        );
    }

    [Fact]
    public async Task AddTask_ShouldNotThrow_WhenNoOverlap()
    {
        // Arrange
        var existingTask = new RoverTask
        {
            RoverName = "Curiosity",
            StartsAt = new DateTime(2025, 1, 1, 9, 0, 0),
            DurationMinutes = 60 // 9:00 - 10:00
        };
        
        var newTask = new RoverTask
        {
            RoverName = "Curiosity",
            StartsAt = new DateTime(2025, 1, 1, 11, 0, 0), // Después
            DurationMinutes = 60 // 11:00 - 12:00
        };

        var mockRepo = new Mock<IRoverRepository>();
        mockRepo.Setup(r => r.GetDailyTasksAsync("Curiosity", newTask.StartsAt.Date))
                .ReturnsAsync(new List<RoverTask> { existingTask });
        
        var service = new RoverService(mockRepo.Object);

        // Act
        var exception = await Record.ExceptionAsync(() => service.AddTaskAsync(newTask));

        // Assert
        exception.Should().BeNull();
    }
}