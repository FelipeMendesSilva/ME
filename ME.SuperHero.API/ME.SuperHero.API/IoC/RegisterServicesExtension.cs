using ME.SuperHero.Application.Handlers.Command;
using ME.SuperHero.Infra.IoC;

namespace ME.SuperHero.API.IoC
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
           services.RegisterInfraServices(configuration);
           services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllHeroisQueryHandler).Assembly));           
        }
    }
}
