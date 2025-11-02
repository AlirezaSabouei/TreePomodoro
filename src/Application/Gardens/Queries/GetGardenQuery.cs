using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Queries;

public record GetGardenQuery : IRequest<Garden>
{
    public required Guid UserId { get; set; }
    public required DateTime Today { get; set; }
}

public class GetGardenQueryHandler(IDocumentStore<Garden> documentStore)
    : IRequestHandler<GetGardenQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenQuery request, CancellationToken cancellationToken)
    {
        var gardens = await documentStore
            .GetAsync(a => a.UserId == request.UserId && a.Year == request.Today.Year && a.Month == request.Today.Month && a.Day == request.Today.Day, cancellationToken);
        return gardens.FirstOrDefault()!;
    }
}