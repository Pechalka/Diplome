using System;
using SimpleCqrs.Eventing;

namespace Domain.Events
{
    public class WorkDeletedEvent : DomainEvent
    {
        public Guid WorkId { get; set; }
    }
}
