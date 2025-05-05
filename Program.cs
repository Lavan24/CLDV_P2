using Microsoft.EntityFrameworkCore;
using EventEaseApplication.Models;
using EventEaseApplication.Services; // ✅ Make sure you include this to access BlobStorageServices

namespace EventEaseApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext
            builder.Services.AddDbContext<EventEaseDataBaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<BlobStorageServices>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("BlobStorage");  // Reads from "ConnectionStrings" section
                var containerName = config.GetValue<string>("BlobSettings:ContainerName");  // Reads from "BlobSettings:ContainerName"

                return new BlobStorageServices(connectionString, containerName);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
