using Application.Common.Data;
using Application.Gardens.Queries;
using MediatR;
using Domain.Entities.Gardens;

namespace Application.Gardens.Commands;

public record CreateGardenCommand : IRequest<Garden>
{
    public Guid UserId { get; set; }
}

public class CreateGardenCommandHandler(
    IRequestHandler<GetGardenQuery, Garden?> getGardenHandler,
    IDocumentStore<Garden> documentStore) 
    : IRequestHandler<CreateGardenCommand, Garden>
{
    public async Task<Garden> Handle(CreateGardenCommand request, CancellationToken cancellationToken)
    {
        var garden = await GetGardenAsync(request,cancellationToken) ?? await CreateGardenAsync(request);
        return garden;
    }
    
    private async Task<Garden?> GetGardenAsync(CreateGardenCommand request,CancellationToken cancellationToken)
    {
        var dateTime = DateTime.Today;
        var query = new GetGardenQuery()
        {
            UserId = request.UserId,
            Year = dateTime.Year,
            Month = dateTime.Month,
            Day = dateTime.Day,
        };
        return await getGardenHandler.Handle(query, cancellationToken);
    }

    private async Task<Garden> CreateGardenAsync(CreateGardenCommand request)
    {
        var today = DateTime.Today;
        var garden = new Garden()
        {
            UserId = request.UserId,
            Year = today.Year,
            Month = today.Month,
            Day = today.Day
        };
        await documentStore.InsertAsync(garden);
        return garden;
    }
}