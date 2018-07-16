namespace Alpha.Services.API.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Alpha.Services.API.Behaviors;
    using Alpha.Services.API.Handlers;
    using Alpha.Services.Services;
    using Fanex.Data.Repository;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using SimpleInjector;

    public static class ApplicationBuilderExtensions
    {
        public static void InitializeContainer(this IApplicationBuilder app, Container container)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            container.RegisterMediator();
            container.RegisterDataAccesses();
            container.RegisterServices();

            // Allow Simple Injector to resolve services from ASP.NET Core.
            container.AutoCrossWireAspNetComponents(app);

            container.Verify();
        }

        private static void RegisterMediator(this Container container)
        {
            var assemblies = GetAssemblies().ToArray();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);

            var notificationHandlerTypes = container.GetTypesToRegister(typeof(INotificationHandler<>), assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });
            container.Register(typeof(INotificationHandler<>), notificationHandlerTypes);

            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(PipelineBehavior<,>)
            });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(OutstandingHandler).GetTypeInfo().Assembly;
        }

        private static void RegisterDataAccesses(this Container container)
        {
            container.RegisterSingleton<IDynamicRepository, DynamicRepository>();
        }

        private static void RegisterServices(this Container container)
        {
            container.RegisterSingleton<CustomerService, CustomerServiceImpl>();
            container.RegisterSingleton<OutstandingService, OutstandingServiceImpl>();
        }
    }
}