using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using Domain.Events.Gardens;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record CompleteTreeCommand : IRequest<Garden>
{
    public required Guid GardenId { get; set; }
    public required TreeState TreeState { get; set; }
}

public class CompleteTreeCommandHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler,
    IMediator mediator)
    : IRequestHandler<CompleteTreeCommand, Garden>
{
    private Garden _garden = null!;
    private CompleteTreeCommand _request;

    public async Task<Garden> Handle(CompleteTreeCommand request, CancellationToken cancellationToken)
    {
        _request = request;
        _garden = await GetGardenAsync(cancellationToken);
        _garden.Trees.First(a => a.TreeState == TreeState.Seed).TreeState = request.TreeState;
        var garden = await UpdateGardenAsync(cancellationToken);
        PublishEvent(garden.Id,_request.TreeState);
        return garden;
    }

    private async Task<Garden> GetGardenAsync(CancellationToken cancellationToken)
    {
        var query = new GetGardenByIdQuery()
        {
            Id = _request.GardenId
        };
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden;
    }
    
    private Task<Garden> UpdateGardenAsync(CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateGardenCommand
        {
            GardenId = _garden.Id,
            Delta = CreateDelta()
        };
        return updateGardenHandler.Handle(updateCommand, cancellationToken);
    }

    private Delta<Garden> CreateDelta()
    {
        var delta = new Delta<Garden>();
        delta.TrySetPropertyValue(nameof(Garden.Trees), _garden.Trees);
        return delta;
    }

    public void PublishEvent(Guid gardenId, TreeState treeState)
    {
        mediator.Publish(new TreeCompletedEvent()
        {
            GardenId = _garden.Id,
            TreeState = _request.TreeState
        });
    }
}