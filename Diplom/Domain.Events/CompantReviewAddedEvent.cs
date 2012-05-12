using System;
using SimpleCqrs.Eventing;

namespace Domain.Events
{

    public class CompantReviewAddedEvent : DomainEvent
    {
        public bool IsGood { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
