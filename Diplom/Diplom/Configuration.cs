using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel;
using Castle.Windsor;
using Domain;
using Domain.CommandHandlers;
using Domain.PersistenceHandlers;
using Domain.ViewModel.Queries;
using Infrastructure.CQRS;
using Infrastructure.EventSourcing;
using Infrastructure.IoC;
using Infrastructure.Reflection;
using Component = Castle.MicroKernel.Registration.Component;

namespace Diplom
{
    public static class Configuration
    {
        public static void Configure()
        {
            var container = new WindsorContainer();


            // add command handlers
            foreach (Type handlers in from t in typeof(CreateComanyCommandHandler).Assembly.GetTypes()
                                      where t.ImplementsGenericDefinition(typeof(ICommandHandler<>))
                                      select t)
            {
                Configure(handlers, container);
            }

            // add event handlers
            foreach (Type handlers in from t in typeof(CompanyAddedEventHandler).Assembly.GetTypes()
                                      where t.ImplementsGenericDefinition(typeof(IHandleEvent<>))
                                      select t)
            {
                Configure(handlers, container);
            }

            var component = Component.For<IGetPageOfCompaniesQuery>().ImplementedBy<GetPageOfCompaniesQuery>();
            container.Kernel.Register(component);

            // Configure aggregate roots
            AggregateRoot.CreateDelegatesForAggregatesIn(typeof(Company).Assembly);

            DependencyResolver.SetResolver(new WindsorDependencyResolver(container)); 
        }

        private static void Configure(Type t, WindsorContainer container)
        {
            var handler = container.GetHandlerForType(t);
            if (handler == null)
            {
                var reg = Component.For(GetAllServiceTypesFor(t)).ImplementedBy(t);
                reg.LifestyleSingleton();

                container.Kernel.Register(reg);
            }
        }

        private static IEnumerable<Type> GetAllServiceTypesFor(Type t)
        {
            return new List<Type>(t.GetInterfaces()) { t };
        }

        private static IHandler GetHandlerForType(this WindsorContainer Container, Type t)
        {
            return Container.Kernel.GetAssignableHandlers(typeof(object))
                .Where(h => h.ComponentModel.Implementation == t)
                .FirstOrDefault();
        }
    }
}