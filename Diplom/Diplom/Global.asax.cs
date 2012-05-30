using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Diplom.Models;
using Domain;
using Domain.PersistenceHandlers;
using Domain.ViewModel;
using Infrastructure;
using SimpleCqrs;
using SimpleCqrs.Utilites;

namespace Diplom
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Companies", // Route name
            //    "{category}/{page}", // URL with parameters
            //    new { controller = "Companies", action = "List", category = UrlParameter.Optional, page = UrlParameter.Optional } // Parameter defaults
            //    );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Companies", action = "List", id = UrlParameter.Optional } // Parameter defaults
            );


          //  Configuration.Configure();
            Runtime = new SimpleCqrsRuntime<WindsorServiceLocator>();
            Runtime.Start();

            DependencyResolver.SetResolver(Runtime.ServiceLocator);

            var container = new WindsorContainer();

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>());
            container.Register(Component.For<IAuthProvider>().ImplementedBy<FormAuthProvider>());

            ControllerBuilder.Current.SetControllerFactory(new CastleControllerFactory(container));

            ReCreateReadModels();
        }


        private static void ReCreateReadModels()
        {
            MongoHelper.GetCollectionOf<CompanyViewModel>().RemoveAll();
            MongoHelper.GetCollectionOf<PhotosCompanyDetailsViewModel>().RemoveAll();
            MongoHelper.GetCollectionOf<ReviewCompanyDetailsViewModel>().RemoveAll();
            MongoHelper.GetCollectionOf<CompanyWorksPage>().RemoveAll();

            var eventPlayer = new DomainEventReplayer(Runtime);
            
            eventPlayer.ReplayEventsForHandlerType(typeof(CompanyAddedEventHandler));
            eventPlayer.ReplayEventsForHandlerType(typeof(CompanyUpdatedEventHandler));
            eventPlayer.ReplayEventsForHandlerType(typeof(CompantReviewAddedEventHandler));
            eventPlayer.ReplayEventsForHandlerType(typeof(WorkDeletedEventHandler));
            eventPlayer.ReplayEventsForHandlerType(typeof(AddWorkEventHandler));
        }


        private static SimpleCqrsRuntime<WindsorServiceLocator> Runtime;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_End()
        {
            Runtime.Shutdown();
        }
    }

    public class CastleControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public CastleControllerFactory(IWindsorContainer container)
        {
            _container = container;
            _container.Register(
                AllTypes.FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifestyleTransient()));
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                return (IController)_container.Resolve(controllerType);
            }
            else
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }
    }
}