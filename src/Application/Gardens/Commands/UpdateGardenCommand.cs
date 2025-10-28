using Application.Common.Data;
using Application.Gardens.Queries;
using Domain;
using Domain.Entities.Gardens;
using Domain.Events.Gardens;
using Force.DeepCloner;
using MediatR;
using Microsoft.AspNetCore.OData.Deltas;

namespace Application.Gardens.Commands;

public record UpdateGardenCommand : IRequest<Garden>
{
    public required Guid GardenId { get; set; }
    public required Delta<Garden> Delta { get; set; }
}

public class UpdateGardenCommandHandler(
    IRequestHandler<GetGardenByIdQuery, Garden> getGardenHandler,
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
        var query = new GetGardenByIdQuery()
        {
            Id = request.GardenId
        };
        return await getGardenHandler.Handle(query, cancellationToken);
    }
}