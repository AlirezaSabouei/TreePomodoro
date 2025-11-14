using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Commands;

public record CompleteSeedCommand : IRequest<Garden>
{
    public required Guid GardenId { get; set; }
}

public class CompleteSeedCommandHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenHandler,
    IRequestHandler<UpdateGardenCommand, Garden> updateGardenHandler)
    : IRequestHandler<CompleteSeedCommand, Garden>
{
    private Garden? _garden;
    
    public async Task<Garden> Handle(CompleteSeedCommand request, CancellationToken cancellationToken)
    {
        _garden = await GetGardenAsync(request, cancellationToken);
        _garden.CompleteSeed();
        var garden = await UpdateGardenAsync(cancellationToken);
        return garden;
    }
    
    private async Task<Garden> GetGardenAsync(CompleteSeedCommand request, CancellationToken cancellationToken)
    {
        var query = new GetGardenByIdQuery()
        {
            Id = request.GardenId
        };
        var garden = await getGardenHandler.Handle(query, cancellationToken);
        return garden;
    }
    
    private async Task<Garden> UpdateGardenAsync(CancellationToken cancellationToken)
    {
        var updateCommand = new UpdateGardenCommand
        {
            Garden = _garden!
        };
        return await updateGardenHandler.Handle(updateCommand, cancellationToken);
    }
}