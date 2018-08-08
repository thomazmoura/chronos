using Chronos.API.Dados;
using Chronos.API.Objetos;
using Microsoft.Extensions.Configuration;
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

        public static void UseChronosConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var configuracaoDeUrls = new ConfiguracaoDeUrls();
            configuration.GetSection("ConfiguracaoDeUrls").Bind(configuracaoDeUrls);

            services.AddSingleton<IConfiguracaoDeUrls, ConfiguracaoDeUrls>((provider) => configuracaoDeUrls);
        }
    }
}