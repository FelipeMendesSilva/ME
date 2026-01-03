using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using ME.SuperHero.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ME.SuperHero.Infra.IoC
{
    public static class RegisterInfraServicesExtension
    {
        public static void RegisterInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<AppDbContext>(options =>
                   options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 43))));

            services.AddScoped<IHeroisRepository, HeroisRepository>();
        }
    }
}
