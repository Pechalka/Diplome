using System;
using SimpleCqrs.Eventing;

namespace Domain.Events
{
    public class CompanyUpdatedEvent : DomainEvent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
    }
}
