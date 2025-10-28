using System.Reflection;
using Application.Common.Behaviours;
using Application.Gardens.Commands;
using Domain.Entities;
using Domain.Entities.Gardens;
using Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient<IRequestHandler<CompleteTreeCommand,Garden>, CompleteTreeCommandHandler>();
        services.AddScoped<SignedUser>(_ => new SignedUser()
        {
            Name = "Alireza Sabouei",
            UserId = new Guid("FB69B0F9-D40E-4985-86FD-BF8513F2CD01")
        });

        //Auto Mapper Configuration
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly()); // scans for Profile classes
        });

        //MediatR Configuration
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        services.AddSignalR();
    }
}