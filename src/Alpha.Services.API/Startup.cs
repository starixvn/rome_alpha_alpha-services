namespace Alpha.Services.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Alpha.Services.API.Extensions;
    using Fanex.Data;
    using Fanex.Logging;
    using Fanex.Logging.RabbitMQ;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Dictionary<string, string> connections = Configuration
               .GetSection("ConnectionStrings")
               .GetChildren()
               .ToDictionary(connection => connection.Key, connection => connection.Value, StringComparer.OrdinalIgnoreCase);

            DbSettingProviderManager
                .StartNewSession()
                .UseConnectionStrings(connections)
                .UseDefaultDbSettingProvider(Configuration["StoredProcedurePath"])
                .Run();

            LogManager
                    .SetDefaultLogCategory(Configuration["Fanex.Logging:DefaultCategory"])
                    .Use(new RabbitMQLogging(Configuration["Fanex.Logging:RabbitMQConnectionStrings"]));

            services.AddCustomMvc()
                .AddCustomSwagger()
                .AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
                .AddDataAccesses()
                .AddServices()
                .AddCustomConfiguration(Configuration)
                .AddMemoryCache()
                .AddCustomAuthentication();

            services.AddSingleton(Logger.Log);

            Logger.Log.Info("Alpha Service has been started up.");
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseMvc();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Alpha.Services V1");
                });
        }
    }
}