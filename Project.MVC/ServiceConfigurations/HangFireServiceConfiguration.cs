using Hangfire;
using Hangfire.Storage.SQLite;

namespace Project.MVC.ServiceConfigurations;

public class HangFireServiceConfiguration : ServiceConfigurationBase
{
    public override void RegisterService(WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(configuration =>
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSQLiteStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddHangfireServer();
    }

    public override void UseService(WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire");
    }
}