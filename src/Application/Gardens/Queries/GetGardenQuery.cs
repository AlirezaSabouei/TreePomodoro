using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Queries;

public record GetGardenQuery : IRequest<Garden>;

public class GetGardenQueryHandler(IDocumentStore<Garden> documentStore, SignedUser signedUser) 
    : IRequestHandler<GetGardenQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        var garden = await documentStore.GetAsync(a=>a.Year == today.Year && a.Month == today.Month && a.Day == today.Day);
        return garden!;
    }
}