using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Business;
using Project.Business.Common.Data;
using Project.MVC.Data;

namespace Project.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<Context>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<Context>();
        builder.Services.AddControllersWithViews();

        // Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        //DI
        var signedUser = new SignedUser()
        {
            Name = "Alireza",
            UserId = new Guid("3D029536-EFC2-479E-A732-BA3A9091890E")
        };
        builder.Services.AddScoped<SignedUser>(_ => signedUser);
        builder.Services.AddScoped<Business.Services.StudentServices>();
        builder.Services.AddScoped<Business.Services.Gardens.GardenServices>();
        //builder.Services.AddScoped<DbContext, Context>();
        //DI
        
        var app = builder.Build();
        
        // Middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<Context>(); // Replace YourDbContext with your actual context class
        
            // Apply any pending migrations
            dbContext.Database.Migrate();
        }

        app.Run();
    }
}