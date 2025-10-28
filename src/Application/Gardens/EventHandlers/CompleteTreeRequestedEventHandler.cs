using Application.Gardens.Commands;
using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using Domain.Events.Gardens;
using MediatR;

namespace Application.Gardens.EventHandlers;

public class CompleteTreeRequestedEventHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenByIdQueryHandler,
    IRequestHandler<CompleteTreeCommand, Garden> completeTreeRequestedHandler)
    : INotificationHandler<CompleteTreeRequestedEvent>
{
    private CompleteTreeRequestedEvent _notification;
    private Garden _garden;

    public async Task Handle(CompleteTreeRequestedEvent notification, CancellationToken cancellationToken)
    {
        _notification = notification;
        _garden = await GetGardenAsync(cancellationToken);
        await CompleteTree(cancellationToken);
    }

    private async Task<Garden> GetGardenAsync(CancellationToken cancellationToken)
    {
        var query = new GetGardenByIdQuery()
        {
            Id = _notification.GardenId
        };
        return await getGardenByIdQueryHandler.Handle(query, cancellationToken);
    }

    private async Task CompleteTree(CancellationToken cancellationToken)
    {
        var command = new CompleteTreeCommand()
        {
            GardenId = _notification.GardenId,
            TreeState = _notification.TreeState
        };
        await completeTreeRequestedHandler.Handle(command, cancellationToken);
    }
}