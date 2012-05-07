using Domain.Events;
using Infrastructure.EventSourcing;

namespace Domain.PersistenceHandlers
{
    public class CompanyAddedEventHandler : IHandleEvent<CompanyAddedEvent>
    {
        public void Handle(CompanyAddedEvent @event)
        {

        }
    }
}
