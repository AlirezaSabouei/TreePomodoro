using System.Linq.Expressions;
using Application.Common.Tools;
using Domain.Entities.Gardens;
using Domain.Events.Gardens;
using Hangfire;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Gardens;

public class MyJob : IJob
{
    private readonly IServiceScopeFactory _scopeFactory;

    public MyJob(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task ExecuteAsync(Guid gardenId)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await mediator.Publish(new CompleteTreeRequestedEvent { GardenId = gardenId,
                TreeState = TreeState.Green});
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void QueueJob(Expression<Action> action, TimeSpan delay)
    {
        BackgroundJob.Schedule(action, delay);
    }
}