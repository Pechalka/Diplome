using System;
using Infrastructure.EventSourcing;

namespace Domain.Events
{
    [Serializable]
    public class CompantReviewAddedEvent : IEvent
    {
        public Guid CompanyId { get; set; }
        public bool IsGood { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
