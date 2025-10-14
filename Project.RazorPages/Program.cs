using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Business;
using Project.Business.Common.Data;

namespace Project.RazorPages
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            //builder.Services.AddDbContext<Context>(options =>
            //    options.UseSqlServer(connectionString));
            // Add SQLite DbContext
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<Context>();
            builder.Services.AddRazorPages();

            // Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //DI
            var signedUser = new SignedUser()
            {
                Name = "Alireza",
                UserId = Guid.NewGuid()
            };
            builder.Services.AddScoped<SignedUser>(_ => signedUser);
            builder.Services.AddScoped<Business.Services.StudentServices>();
            builder.Services.AddScoped<Business.Services.Gardens.GardenServices>();
            builder.Services.AddScoped<IContext, Context>();
            //DI

            var app = builder.Build();

            app.MapControllers();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
