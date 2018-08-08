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
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ContextoDeDadosChronos");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ContextoDeDadosChronos>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

            services.UseChronosDependencies();
            services.UseChronosConfiguration(Configuration);

            services.AddMvc(options =>
            {
                options.Filters.Add<UnitOfWorkFilter>();
                options.Filters.Add<IQueryableIteratorFilter>();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguracaoDeUrls configuracaoDeUrls)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins(configuracaoDeUrls.UrlFrontEnd);
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseMvc();
        }
    }
}
