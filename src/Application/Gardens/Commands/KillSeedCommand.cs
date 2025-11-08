using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Commands;

public record KillSeedCommand : IRequest<Garden>
{
    public required Guid GardenId { get; set; }
}

public class KillSeedCommandHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler)
    : IRequestHandler<KillSeedCommand, Garden>
{
    private Garden _garden = null!;

    public async Task<Garden> Handle(KillSeedCommand request, CancellationToken cancellationToken)
    {
        _garden = await GetGardenAsync(request, cancellationToken);
        _garden.KillSeed();
        _garden = await UpdateGardenAsync(cancellationToken);
        return _garden;
    }

    private async Task<Garden> GetGardenAsync(KillSeedCommand request, CancellationToken cancellationToken)
    {
        var query = new GetGardenByIdQuery()
        {
            Id = request.GardenId
        };
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden;
    }

    private Task<Garden> UpdateGardenAsync(CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateGardenCommand
        {
            Garden = _garden
        };
        return updateGardenHandler.Handle(updateCommand, cancellationToken);
    }
}