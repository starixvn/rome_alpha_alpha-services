namespace Alpha.Services.API.Extensions
{
    using Alpha.Services.API.Infrastructure.Auth;
    using Alpha.Services.API.Infrastructure.Filters;
    using Alpha.Services.Configurations;
    using Alpha.Services.Services;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Fanex.RemotingRef;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
                });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "Alpha Services",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.AddSingleton<IAppSettings>(new AppSettings(configuration));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.AddScheme("HMAC", auth => auth.HandlerType = typeof(HmacAuthenticationHandler));
            });

            return services;
        }

        public static IServiceCollection AddDataAccesses(this IServiceCollection services)
        {
            services.AddSingleton<IDynamicRepository, DynamicRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<CustomerService, CustomerServiceImpl>();
            services.AddSingleton<OutstandingService, OutstandingServiceImpl>();
            services.AddSingleton<ReportDateService, ReportDateServiceImpl>();
            services.AddSingleton<BalanceInfoService, BalanceInfoServiceImpl>();
            services.AddSingleton<ProductService, ProductServiceImpl>();
            services.AddSingleton<WinLossService, WinLossServiceImpl>();
            services.AddSingleton<MessageService, MessageServiceImpl>();
            services.AddSingleton<LogService, LogServiceImpl>();
            services.AddSingleton<OutstandingService, OutstandingServiceImpl>();
            services.AddSingleton<IRemotingRefService, RemotingRefService>();

            return services;
        }
    }
}