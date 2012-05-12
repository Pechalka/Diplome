using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Domain.ViewModel;
using Infrastructure.CQRS;
using SimpleCqrs;
using SimpleCqrs.Commanding;
using SimpleCqrs.EventStore.MongoDb;
using SimpleCqrs.Eventing;

namespace Diplom
{
    public class WindsorServiceLocator : IServiceLocator, System.Web.Mvc.IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorServiceLocator(IWindsorContainer container)
        {
            _container = container;

            //container.Register(
            //    AllTypes.FromThisAssembly().Pick().WithService.DefaultInterfaces(),
            //    AllTypes.FromAssemblyNamed("SimpleCqrs").Pick().WithService.DefaultInterfaces()
            //             );

            //  _container.Register(AllTypes.FromAssemblyNamed("SimpleCqrs").Pick().WithService.FirstInterface());


            _container.Register(Component.For<DomainEventHandlerFactory>());

            _container.Register(AllTypes.FromAssemblyNamed("Domain.ViewModel").BasedOn(typeof(IQuery<>)).WithService.DefaultInterfaces().Configure(x => x.LifestyleTransient()));

            // string eventDataBaseConnectionString = "";
            // _container.Register(Component.For<IEventStore>().ImplementedBy<MongoEventStore>().DependsOn(Property.ForKey("connectionString").Eq(eventDataBaseConnectionString)));
            // _container.Register(Component.For<ISnapshotStore>().ImplementedBy<MongoSnapshotStore>().DependsOn(Property.ForKey("connectionString").Eq(eventDataBaseConnectionString)));

            _container.Register(Component.For<IEventStore>().ImplementedBy<MongoEventStore>().DependsOn(Property.ForKey("connectionString").Eq("mongodb://localhost")));
            // _container.Register(Component.For<ISnapshotStore>().ImplementedBy<NullSnapshotStore>());



            _container.Register(AllTypes.FromAssemblyNamed("Domain.PersistenceHandlers").BasedOn(typeof(IHandleDomainEvents<>)).WithService.DefaultInterfaces().Configure(x => x.LifestyleTransient()));
            _container.Register(AllTypes.FromAssemblyNamed("Domain.CommandHandlers").BasedOn(typeof(CommandHandler<>)).WithService.DefaultInterfaces().Configure(x => x.LifestyleTransient()));
        }

        public WindsorServiceLocator()
            : this(new WindsorContainer())
        {
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public IList<T> ResolveServices<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface>(Type implType) where TInterface : class
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface, TImplementation>() where TImplementation : class, TInterface
        {
            _container.Register(Component.For(typeof(TInterface)).ImplementedBy<TImplementation>());
        }

        public void Register<TInterface, TImplementation>(string key) where TImplementation : class, TInterface
        {
            throw new NotImplementedException();
        }

        public void Register(string key, Type type)
        {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Type implType)
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface>(TInterface instance) where TInterface : class
        {
            try
            {
                _container.Register(Component.For<TInterface>().Instance(instance));
            }
            catch (ComponentRegistrationException ex)
            {
            }

        }

        public void Release(object instance)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public TService Inject<TService>(TService instance) where TService : class
        {
            throw new NotImplementedException();
        }

        public void TearDown<TService>(TService instance) where TService : class
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface>(Func<TInterface> factoryMethod) where TInterface : class
        {
            _container.Register(Component.For<TInterface>().UsingFactoryMethod(factoryMethod));
        }

        //-----------------------

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }
    }
}
