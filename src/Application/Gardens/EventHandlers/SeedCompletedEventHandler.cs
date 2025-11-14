using Domain.Events.Gardens;
using MediatR;

namespace Application.Gardens.EventHandlers;

public class SeedCompletedEventHandler(
    NotificationService notificationService) : INotificationHandler<SeedCompletedEvent>
{
    public Task Handle(SeedCompletedEvent notification, CancellationToken cancellationToken)
    {
        notificationService.Notify(notification.UserId, notification.GardenId.ToString());
        return Task.CompletedTask;
    }
}