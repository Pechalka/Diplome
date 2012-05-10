using System;
using Domain.Commands;
using Domain.Events;
using Infrastructure.EventSourcing;

namespace Domain
{
    public class Company : AggregateRoot
    {
        public Company(Guid id, string name, string description)
            : base(id)
        {
            ApplyEvent(new CompanyAddedEvent { CompanyId = id, Name = name, Description = description });
        }

        public Company(IEventInputStream events) : base(events)
        {
        }

        public string Category { get; set; }

        public string Description { get; set; }

        public string WorkedTime { get; set; }

        public string Address { get; set; }

        public string LogoImg { get; set; }


        public string Name { get; set; }


        public void Update(string newName, string newDescription, string newAddress)
        {
            ApplyEvent(new CompanyUpdatedEvent{ Id = Id, Name = newName, Address = newAddress, Description = newDescription });
        }

        private void Apply(CompanyUpdatedEvent evt)
        {
            Description = evt.Description;
            Address = evt.Address;
            Name = evt.Name;
        }

        private void Apply(CompanyAddedEvent evt)
        {
            Name = evt.Name;
        }

        public void AddReview(AddReviewToCompanyCommand command)
        {
            ApplyEvent(new CompantReviewAddedEvent
                           {
                               CompanyId = Id, 
                               Date = DateTime.Now, 
                               IsGood = command.Type == AddReviewToCompanyCommand.ReviewType.Good, 
                               Text = command.Text
                           });
        }

        private void Apply(CompantReviewAddedEvent evt)
        {
        }
    }
}