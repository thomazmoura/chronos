using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronos.API.Dados;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("ContextoDeDadosChronos");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ContextoDeDadosChronos>(options =>
                {
                    options.UseNpgsql(connectionString);
                });

            //TODO Implementar extensão de injeção de dependência
            //services.UseChronosDependencies();

            services.AddMvc(options =>
            {
                //TODO Implementar UnitOfWork e Iterador
                //options.Filters.Add<UnitOfWorkFilter>();
                //options.Filters.Add<IQueryableIteratorFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
