using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Gardens.Queries;

public record GetGardenByIdQuery : IRequest<Garden>
{
    public Guid Id { get; set; }
}

public class GetGardenByIdQueryHandler(IContext context) 
    : IRequestHandler<GetGardenByIdQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenByIdQuery request, CancellationToken cancellationToken)
    {
        var garden = await context.Gardens
            .Where(a => a.Id == request.Id)
            .Include(a => a.Trees)
            .FirstAsync(cancellationToken);
        return garden!;
    }
}