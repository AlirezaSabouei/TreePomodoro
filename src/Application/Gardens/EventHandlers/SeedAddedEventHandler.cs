using Application.Gardens.Jobs;
using Domain.Events.Gardens;
using Hangfire;
using MediatR;

namespace Application.Gardens.EventHandlers;

public class SeedAddedEventHandler : INotificationHandler<SeedAddedEvent>
{
    public Task Handle(SeedAddedEvent notification, CancellationToken cancellationToken)
    {
        BackgroundJob.Schedule<CompleteSeedJob>(job => job.ExecuteAsync(notification.GardenId),
            TimeSpan.FromSeconds(notification.GrowthTimeInSeconds));
        return Task.CompletedTask;
    }
}