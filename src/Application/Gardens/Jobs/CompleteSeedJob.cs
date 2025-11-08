using Application.Gardens.Commands;
using Domain.Entities.Gardens;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Gardens.Jobs;

public class CompleteSeedJob(IServiceScopeFactory scopeFactory)
{
    public async Task ExecuteAsync(Guid gardenId)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<CompleteSeedCommand, Garden>>();
            var command = new CompleteSeedCommand()
            {
                GardenId = gardenId
            };
            await handler.Handle(command, CancellationToken.None);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}