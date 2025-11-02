using Application.Common.Data;
using Domain.Entities.Glasses;
using MediatR;

namespace Application.Glasses.Queries;

public record GetGlassesQuery : IRequest<List<Glass>>
{
    public required Guid UserId { get; set; }
    public required DateTime Today { get; set; }
}

public class GetGlassesQueryHandler(IDocumentStore<Glass> documentStore) : IRequestHandler<GetGlassesQuery, List<Glass>>
{
    public async Task<List<Glass>> Handle(GetGlassesQuery request, CancellationToken cancellationToken)
    {
        var glasses = await documentStore.GetAsync(a =>
            a.UserId == request.UserId &&
            a.Year == request.Today.Year &&
            a.Month == request.Today.Month &&
            a.Day == request.Today.Day,
            cancellationToken);
        return glasses;
    }
}