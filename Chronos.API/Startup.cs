using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronos.API.Dados;
using Chronos.API.Filters;
using Chronos.API.IoC;
using Chronos.API.Objetos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Chronos.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ContextoDeDadosChronos");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ContextoDeDadosChronos>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

            services.UseChronosDependencies();

            services.AddMvc(options =>
            {
                options.Filters.Add<UnitOfWorkFilter>();
                options.Filters.Add<IQueryableIteratorFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var configuracaoDeUrls = new ConfiguracaoDeUrls();
            Configuration.GetSection("ConfiguracaoDeUrls").Bind(configuracaoDeUrls);

            app.UseCors(builder => builder.WithOrigins(configuracaoDeUrls.UrlFrontEnd));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
