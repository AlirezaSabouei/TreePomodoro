using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;

namespace Application.Gardens.Queries;

public record GetGardenQuery : IRequest<Garden>
{
    public required Guid UserId { get; set; }
    public required int Year { get; set; }
    public required int Month { get; set; }
    public required int Day { get; set; }
}

public class GetGardenQueryHandler(IDocumentStore<Garden> documentStore) 
    : IRequestHandler<GetGardenQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenQuery request, CancellationToken cancellationToken)
    {
        var gardens = await documentStore
            .GetAsync(a=>
                a.Year == request.Year &&
                a.Month == request.Month &&
                a.Day == request.Day &&
                a.UserId == request.UserId
                ,cancellationToken);
        return gardens.FirstOrDefault()!;
    }
}