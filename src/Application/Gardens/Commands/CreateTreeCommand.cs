using Application.Common.Tools;
using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using Domain.Events.Gardens;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record CreateTreeCommand : IRequest<Garden>
{
    public required Guid GardenId { get; set; }
    public required int GrowthTimeInSeconds { get; set; }
}

public class CreateTreeCommandHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler,
    IMediator mediator)
    : IRequestHandler<CreateTreeCommand, Garden>
{
    private Garden _garden = null!;
    private CreateTreeCommand _request;

    public async Task<Garden> Handle(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        _request = request;
        _garden = await GetGardenAsync(request, cancellationToken);
        ValidateGardenHasNoMoreThan25Trees();
        AddASeedTree();
        var garden = await UpdateGardenAsync(cancellationToken);
        //TODO: it is better I move it out of command handler
        //job.QueueJob(() => PublishTreeCreatedEvent(garden.Id),TimeSpan.FromSeconds(10));

        BackgroundJob.Schedule<MyJob>(job => job.ExecuteAsync(garden.Id),
            TimeSpan.FromSeconds(request.GrowthTimeInSeconds));
        return garden;
    }

    private async Task<Garden> GetGardenAsync(CreateTreeCommand request, CancellationToken cancellationToken)
    {
        var query = new GetGardenByIdQuery()
        {
            Id = request.GardenId
        };
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden;
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
            PlantedDate = DateTime.Now,
            GrowthTimeInSeconds = _request.GrowthTimeInSeconds
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

    public void PublishTreeCreatedEvent(Guid gardenId)
    {
        //Note : this method should be public for hangfire to be able to work with it
        var treeCreatedEvent = new CompleteTreeRequestedEvent()
        {
            GardenId = gardenId,
            TreeState = TreeState.Green
        };
        mediator.Publish(treeCreatedEvent);
    }
}