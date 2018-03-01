using Chronos.API.Dados;
using Microsoft.Extensions.DependencyInjection;

namespace Chronos.API.IoC
{
    public static class DependencyInjection
    {
        public static void UseChronosDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositorio, RepositorioPostgresql>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}