using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Queries;

public record GetGardenByIdQuery : IRequest<Garden>
{
    public required Guid Id { get; set; }
}

public class GetGardenByIdQueryHandler(IDocumentStore<Garden> documentStore) 
    : IRequestHandler<GetGardenByIdQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenByIdQuery request, CancellationToken cancellationToken)
    {
        var garden = await documentStore
            .GetByIdAsync(request.Id, cancellationToken);   
        return garden!;
    }
}