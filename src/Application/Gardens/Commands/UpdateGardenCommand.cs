using Application.Common.Data;
using Application.Gardens.Queries;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record UpdateGardenCommand : IRequest<Garden>
{
    public required Delta<Garden> Delta { get; set; }
}

public class UpdateGardenCommandHandler(
    IRequestHandler<GetGardenQuery, Garden> getGardenHandler,
    IDocumentStore<Garden> documentStore) 
    : IRequestHandler<UpdateGardenCommand, Garden>
{
    public async Task<Garden> Handle(UpdateGardenCommand request, CancellationToken cancellationToken)
    {
        var garden = await GetGardenAsync(request, cancellationToken);
        request.Delta.Patch(garden);
        await documentStore.UpdateAsync(garden);
        return garden;
    }

    private async Task<Garden> GetGardenAsync(UpdateGardenCommand request, CancellationToken cancellationToken)
    {
        var query = new GetGardenQuery();
        return await getGardenHandler.Handle(query, cancellationToken);
    }
}