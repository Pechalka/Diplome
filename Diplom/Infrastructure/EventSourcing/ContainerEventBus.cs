using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Infrastructure.EventSourcing
{
    public class ContainerEventBus : IEventBus
    {
        public void Publish<T>(T evt) where T : IEvent
        {
            var handlerType = typeof(IHandleEvent<>).MakeGenericType(evt.GetType());

            foreach (var handler in BuildAll<object>(handlerType))
            {
                handlerType.GetMethod("Handle").Invoke(handler, new object[] { evt });
            }
        }

        public IEnumerable<T> BuildAll<T>(Type type)
        {
            return DependencyResolver.Current.GetServices(type).Cast<T>();
        }

    }
}
