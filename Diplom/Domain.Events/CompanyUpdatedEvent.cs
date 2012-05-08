using System;
using Infrastructure.EventSourcing;

namespace Domain.Events
{
    [Serializable]
    public class CompanyUpdatedEvent : IEvent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid Id { get; set; }
    }
}
