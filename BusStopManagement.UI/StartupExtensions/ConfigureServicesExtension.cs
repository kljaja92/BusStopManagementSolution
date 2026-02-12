using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.ServiceContracts;
using BusStopManagement.Core.Services;
using BusStopManagement.Infrastructure.DatabaseContext;
using BusStopManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.UI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IDepartureRepository, DepartureRepository>();
            services.AddScoped<IBusStopRepository, BusStopRepository>();
            services.AddScoped<IDepartureAdderService, DepartureAdderService>();
            services.AddScoped<IDepartureDeleterService, DepartureDeleterService>();
            services.AddScoped<IDepartureGetterService, DepartureGetterService>();
            services.AddScoped<IDepartureUpdaterService, DepartureUpdaterService>();

            return services;
        }
    }
}
