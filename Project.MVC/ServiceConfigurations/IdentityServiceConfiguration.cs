using Microsoft.AspNetCore.Identity;
using Project.Business.Common.Data;

namespace Project.MVC.ServiceConfigurations;

public class IdentityServiceConfiguration : ServiceConfigurationBase
{
    public override void RegisterService(WebApplicationBuilder builder)
    {
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<Context>();
    }

    public override void UseService(WebApplication app)
    {
        app.UseAuthorization();
    }
}