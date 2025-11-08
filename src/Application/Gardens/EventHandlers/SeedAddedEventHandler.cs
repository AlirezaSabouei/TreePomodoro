using Application.Gardens.Jobs;
using Domain.Events.Gardens;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Gardens.EventHandlers;

public class SeedAddedEventHandler(NotificationService notificationService) : INotificationHandler<SeedAddedEvent>
{
    public async Task Handle(SeedAddedEvent notification, CancellationToken cancellationToken)
    {
        BackgroundJob.Schedule<CompleteSeedJob>(job => job.ExecuteAsync(notification.GardenId),
            TimeSpan.FromSeconds(notification.GrowthTimeInSeconds));
        
        //notificationService.Notify(notification.UserId, notification.GardenId.ToString());
    }
}