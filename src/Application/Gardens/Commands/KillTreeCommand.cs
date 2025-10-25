using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record KillTreeCommand : IRequest<Garden>;

public class KillTreeCommandHandler(
    IRequestHandler<GetGardenQuery, Garden?> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler)
    : IRequestHandler<KillTreeCommand, Garden>
{
    private Garden _garden = null!;

    public async Task<Garden> Handle(KillTreeCommand request, CancellationToken cancellationToken)
    {
        _garden = await GetGardenAsync(cancellationToken);
        KillATree();
        var garden = await UpdateGardenAsync(cancellationToken);
        return garden;
    }
    
    private async Task<Garden> GetGardenAsync(CancellationToken cancellationToken)
    {
        var query = new GetGardenQuery();
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden!;
    }
    
    private void KillATree()
    {
        var treeToKill = _garden.Trees.First(a => a.TreeState == TreeState.Seed);
        treeToKill.TreeState = TreeState.Dry;
    }
    
    private Task<Garden> UpdateGardenAsync(CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateGardenCommand
        {
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
}