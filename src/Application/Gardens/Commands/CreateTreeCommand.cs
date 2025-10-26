using Application.Common.Data;
using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record CreateTreeCommand : IRequest<Garden>;

public class CreateTreeCommandHandler(
    IRequestHandler<GetGardenQuery, Garden?> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler)
    : IRequestHandler<CreateTreeCommand, Garden>
{
    private Garden _garden = null!;

    public async Task<Garden> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        _garden = await GetGardenAsync(cancellationToken);
        ValidateGardenHasNoMoreThan25Trees();
        AddASeedTree();
        var garden = await UpdateGardenAsync(cancellationToken);
        return garden;
    }
    
    private async Task<Garden> GetGardenAsync(CancellationToken cancellationToken)
    {
        var query = new GetGardenQuery();
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden!;
    }
    
    private void ValidateGardenHasNoMoreThan25Trees()
    {
        if (_garden.Trees.Count == 25 && _garden.Trees.All(a => a.TreeState is TreeState.Green or TreeState.Dry))
        {
            throw new Exception("Your garden is full for today! Get some rest!");
        }
    }
    
    private void AddASeedTree()
    {
        var seed = _garden.Trees.FirstOrDefault(a => a.TreeState == TreeState.Seed);
        if (seed == null)
        {
            seed = CreateANewSeed();
            _garden.Trees.Add(seed);
        }
    }

    private Tree CreateANewSeed()
    {
        var seed = new Tree
        {
            Index = CreateARandomIndexForTheTree(),
            TreeState = TreeState.Seed,
            PlantedDate = DateTime.Now
        };

        return seed;
    }

    private int CreateARandomIndexForTheTree()
    {
        var treeIndexes = _garden.Trees.Select(x => x.Index).ToList();
        var random = new Random();
        var randomIndex = random.Next(0, 25);
        while (treeIndexes.Contains(randomIndex))
        {
            randomIndex = random.Next(0, 25);
        }
        return randomIndex;
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