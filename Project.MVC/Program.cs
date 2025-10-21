using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Business;
using Project.Business.Common.Data;
using Project.MVC.Data;
using Project.MVC.ServiceConfigurations;

namespace Project.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder);


        builder.Services.AddControllersWithViews();
        
        var app = builder.Build();
        
        UseServices(app);

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



        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();


        app.Run();
    }

    private static void AddServices(WebApplicationBuilder builder)
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Find all types assignable to MyClass (including MyClass itself)
        var types = assembly.GetTypes()
            .Where(t => typeof(ServiceConfigurationBase).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var type in types)
        {
            // Create an instance
            var instance = Activator.CreateInstance(type) as ServiceConfigurationBase;

            // Call method X
            instance?.RegisterService(builder);
        }
    }
    
    private static void UseServices(WebApplication app)
    {
        // Get the current assembly
        var assembly = Assembly.GetExecutingAssembly();

        // Find all types assignable to MyClass (including MyClass itself)
        var types = assembly.GetTypes()
            .Where(t => typeof(ServiceConfigurationBase).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var type in types)
        {
            // Create an instance
            var instance = Activator.CreateInstance(type) as ServiceConfigurationBase;

            // Call method X
            instance?.UseService(app);
        }
    }
}