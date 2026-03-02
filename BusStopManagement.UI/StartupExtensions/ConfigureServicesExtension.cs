using BusStopManagement.Core.Domain.IdentityEntities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.ServiceContracts;
using BusStopManagement.Core.Services;
using BusStopManagement.Infrastructure.DatabaseContext;
using BusStopManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            services.AddScoped<IBusStopAdderService, BusStopAdderService>();
            services.AddScoped<IBusStopGetterService, BusStopGetterService>();
            services.AddScoped<IBusStopDeleterService, BusStopDeleterService>();
            services.AddScoped<IBusStopUpdaterService, BusStopUpdaterService>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>().AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

            return services;
        }
    }
}
