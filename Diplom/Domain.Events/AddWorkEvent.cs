using System;
using SimpleCqrs.Eventing;

namespace Domain.Events
{
    public class AddWorkEvent : DomainEvent
    {
        public string WorkTitle { get; set; }
        public string WorkText { get; set; }
        public Guid WorkId { get; set; }
    }
}
