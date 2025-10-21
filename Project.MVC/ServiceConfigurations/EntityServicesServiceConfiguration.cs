namespace Project.MVC.ServiceConfigurations;

public class EntityServicesServiceConfiguration : ServiceConfigurationBase
{
    public override void RegisterService(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<Business.Services.StudentServices>();
    }

    public override void UseService(WebApplication app)
    {

    }
}