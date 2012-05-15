using System;
using SimpleCqrs.Eventing;

namespace Domain.Events
{
    public class CompanyAddedEvent : DomainEvent
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
