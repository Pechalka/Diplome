using System;
using Infrastructure.EventSourcing;

namespace Domain.Events
{
    [Serializable]
    public class CompanyAddedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
