namespace Project.MVC.ServiceConfigurations;

public abstract class ServiceConfigurationBase
{
    public abstract void RegisterService(WebApplicationBuilder builder);
    
    public abstract void UseService(WebApplication app);
}