namespace Project.MVC.ServiceConfigurations;

public class EntityServicesServiceConfiguration : ServiceConfigurationBase
{
    public override void RegisterService(WebApplicationBuilder builder)
    {
        var signedUser = new Business.SignedUser()
        {
            UserId = Guid.Empty,
            Name = "Parisa"
        };
        builder.Services.AddScoped<Business.SignedUser>(_ => signedUser);
        builder.Services.AddScoped<Business.Services.StudentServices>();
        builder.Services.AddScoped<Business.Services.Gardens.GardenServices>();
    }

    public override void UseService(WebApplication app)
    {
    }
}