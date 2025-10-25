using Application.Common.Data;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Gardens.Queries;

public record GetGardenQuery : IRequest<Garden>;

public class GetGardenQueryHandler(IContext context, SignedUser signedUser) 
    : IRequestHandler<GetGardenQuery, Garden>
{
    public async Task<Garden> Handle(GetGardenQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.Today;
        var garden = await context.Gardens
            .Where(a => a.UserId == signedUser.UserId)
            .Where(a=>a.Year == today.Year && a.Month == today.Month && a.Day == today.Day)
            .Include(a => a.Trees)
            .FirstOrDefaultAsync(cancellationToken);
        return garden!;
    }
}