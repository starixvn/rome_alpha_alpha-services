namespace Alpha.Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Services.API.Extensions;
    using Fanex.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector;

    public class Startup
    {
        private Container container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Dictionary<string, string> connections = Configuration
               .GetSection("ConnectionStrings")
               .GetChildren()
               .ToDictionary(connection => connection.Key, connection => connection.Value, StringComparer.OrdinalIgnoreCase);

            DbSettingProviderManager
                .StartNewSession()
                .UseConnectionStrings(connections)
                .UseDefaultDbSettingProvider("~/App_Data/SPNames/Local")
                .Run();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureCors()
                .IntegrateSimpleInjector(container)
                .AddMemoryCache()
                .AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.InitializeContainer(container);

            app.UseMvc();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Alpha.Services V1");
                });
        }
    }
}