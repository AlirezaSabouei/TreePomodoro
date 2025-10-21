using Microsoft.EntityFrameworkCore;
using Project.Business.Common.Data;

namespace Project.MVC.ServiceConfigurations;

public class EntityFrameworkServiceConfiguration : ServiceConfigurationBase
{
    public override void RegisterService(WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddDbContext<Context>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddScoped<DbContext, Context>();
    }

    public override void UseService(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<Context>(); // Replace YourDbContext with your actual context class
        
            // Apply any pending migrations
            dbContext.Database.Migrate();
        }
    }
}