using System.Reflection;
using Application.Common.Behaviours;
using Application.Students.Commands;
using Application.Students.EventHandlers;
using Domain.Entities;
using Domain.Events;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Application;

public static class ConfigureServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateStudentRequest,Student>,CreateStudentRequestHandler>();
        services.AddScoped<INotificationHandler<StudentCreatedEvent>, StudentCreatedEventHandler>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        //Auto Mapper Configuration
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly()); // scans for Profile classes
        });
        
        //MediatR Configuration
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
           // cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });
        
        //In-Memory Caching Configuration
        services.AddMemoryCache();
    }
    

}