using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.Application.Services;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Infraestructure.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Reflection;
using System.Web.Http;

namespace Kallsonys.PICA.Application
{
    public static class IoC
    {
        private static readonly Container container = new Container();

        private static Container Container => container;

        public static void RegistrarTipos(HttpConfiguration configuracion)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();
            RegistrarRepositorios(assemblies);
            RegistrarServicios();
            RegistrarComandos();
            RegistrarConsultas();
            RegistrarControllers(configuracion);
            Container.Verify();

            // Container.InterceptWith<MonitoringInterceptor>(serviceType => serviceType.Name.StartsWith("Repository"));
            // container.RegisterSingleton<MonitoringInterceptor>();
        }

        private static void RegistrarControllers(HttpConfiguration configuracion)
        {
            Container.RegisterWebApiControllers(configuracion);
            configuracion.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public static TServicio Resolver<TServicio>() where TServicio : class
        {
            return Container.GetInstance<TServicio>();
        }

        private static void RegistrarConsultas()
        {
            //Container.Register<IQueryDispatcher, QueryDispatcher>();
            //Container.Register<IQueryHandler<QueryObtenerSolicitud, Solicitud>, SolicitudesQueryHandler>(ScopedLifestyle.Scoped);
        }

        private static void RegistrarComandos()
        {
            //Container.Register<ICommandDispatcher, CommandDispatcher>();
        }

        private static void RegistrarServicios()
        {
            Container.Register<IOfferService, OfferService>(ScopedLifestyle.Scoped);
            Container.Register<IProductService, ProductService>(ScopedLifestyle.Scoped);
            Container.Register<IImageService, ImageService>(ScopedLifestyle.Scoped);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="assemblies"></param>
        private static void RegistrarRepositorios(Assembly[] assemblies)
        {
            Container.Register<IProductRepository, ProductRepository>(ScopedLifestyle.Scoped);
            Container.Register<IOfferRepository, OfferRepository>(ScopedLifestyle.Scoped);
        }
    }
}