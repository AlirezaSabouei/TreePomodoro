using Application.Common.Data;
using Application.Common.Tools;
using Domain;
using Domain.Entities.Gardens;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Infrastructure.Common.Data;
using Infrastructure.JobTools;
using Infrastructure.PasswordTools;
using Infrastructure.Tools;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDocumentStore<Garden>, DocumentStore<Garden>>();
        
        //MongoDB
        // Register MongoClient as singleton
        services.AddSingleton<IMongoClient>(sp => new MongoClient(configuration.GetConnectionString("MongoConnection")));

        // Register IMongoDatabase as scoped (or singleton)
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase("Garden");
        });
        
        //Email Configuration
        var senderEmail = configuration.GetSection("Email")["Address"]!;
        var senderPassword = configuration.GetSection("Email")["Password"]!;
        services.AddScoped<IEmail>(s=> new Email(senderEmail, senderPassword));

        // Hangfire Configuration
        services.AddHangfire(x => x.UseMongoStorage(configuration.GetConnectionString("MongoConnection")));
        services.AddHangfireServer();
        services.AddScoped<IJob, Job>();

        var mongoStorageOptions = new MongoStorageOptions
        {
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new MigrateMongoMigrationStrategy()
            }
        };

        GlobalConfiguration.Configuration
            .UseMongoStorage("mongodb://localhost:27017/Garden", mongoStorageOptions);
        
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
    }
    
    public static void UseInfrastructureServices(this IApplicationBuilder builder)
    {
        builder.UseHangfireDashboard("/hangfire");
    }
}