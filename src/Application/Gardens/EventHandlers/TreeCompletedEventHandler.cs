using Domain.Events.Gardens;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Application.Gardens.EventHandlers;

public class TreeCompletedEventHandler(IHubContext<GardenHub, IGardenClient> hubContext) : INotificationHandler<TreeCompletedEvent>
{
    public async Task Handle(TreeCompletedEvent notification, CancellationToken cancellationToken)
    {
        await hubContext.Clients
            .Group(notification.GardenId.ToString())
            .TreeCompleted(notification.GardenId, notification.TreeState);
    }
}