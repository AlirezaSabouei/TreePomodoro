using Application.Common.Data;
using Application.Common.Tools;
using Domain;
using Hangfire;
using Hangfire.SQLite;
using Infrastructure.Common.Data;
using Infrastructure.JobTools;
using Infrastructure.PasswordTools;
using Infrastructure.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework Configuration
        services.AddDbContext<Context>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        //services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IContext, Context>();
        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<Context>();
        
        //Email Configuration
        var senderEmail = configuration.GetSection("Email")["Address"]!;
        var senderPassword = configuration.GetSection("Email")["Password"]!;
        services.AddScoped<IEmail>(s=> new Email(senderEmail, senderPassword));

        // Hangfire Configuration
        services.AddHangfire(x => x.UseSQLiteStorage(configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();
        services.AddScoped<IJob, Job>();
        
        // Password Encryption Configuration
        services.AddScoped<IPasswordEncryption<BaseEntity>, PasswordEncryption<BaseEntity>>();
        services.AddScoped<IPasswordHasher<BaseEntity>, PasswordHasher<BaseEntity>>();
        
        //Datetime Provider Configuration
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        
        //token service configuration
        services.AddScoped<ITokenService, TokenService>();
    }
    
    public static void UseInfrastructureServices(this IHost app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<Context>(); // Replace YourDbContext with your actual context class
        
            // Apply any pending migrations
            dbContext.Database.Migrate();
        }
    }
    
    public static void UseInfrastructureServices(this IApplicationBuilder builder)
    {
        builder.UseHangfireDashboard("/hangfire");
    }
}