using Application.Common.Data;
using Application.Gardens.Queries;
using MediatR;
using Domain.Entities.Gardens;

namespace Application.Gardens.Commands;

public record CreateGardenCommand : IRequest<Garden>;

public class CreateGardenCommandHandler(
    IRequestHandler<GetGardenQuery, Garden?> getGardenHandler,
    IContext context,
    SignedUser signedUser) 
    : IRequestHandler<CreateGardenCommand, Garden>
{
    public async Task<Garden> Handle(CreateGardenCommand request, CancellationToken cancellationToken)
    {
        var garden = await GetGardenAsync(cancellationToken) ?? await CreateGardenAsync();
        return garden;
    }
    
    private async Task<Garden?> GetGardenAsync(CancellationToken cancellationToken)
    {
        var query = new GetGardenQuery();
        return await getGardenHandler.Handle(query, cancellationToken);
    }

    private async Task<Garden> CreateGardenAsync()
    {
        var today = DateTime.Today;
        var garden = new Garden()
        {
            UserId = signedUser.UserId,
            Year = today.Year,
            Month = today.Month,
            Day = today.Day
        };
        await context.Gardens.AddAsync(garden);
        await context.SaveChangesAsync();
        return garden;
    }
}