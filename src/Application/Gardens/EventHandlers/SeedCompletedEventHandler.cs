using Domain.Events.Gardens;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Gardens.EventHandlers;

public class SeedCompletedEventHandler(
    NotificationService notificationService) : INotificationHandler<SeedCompletedEvent>
{
    public async Task Handle(SeedCompletedEvent notification, CancellationToken cancellationToken)
    {
        notificationService.Notify(notification.UserId, notification.GardenId.ToString());
    }
}