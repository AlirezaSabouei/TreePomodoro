using Domain.Events.Gardens;
using MediatR;

namespace Application.Gardens.EventHandlers;

public class SeedKilledEventHandler() : INotificationHandler<SeedKilledEvent>
{
    public async Task Handle(SeedKilledEvent notification, CancellationToken cancellationToken)
    {
    }
}