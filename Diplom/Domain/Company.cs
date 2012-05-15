using System;
using Domain.Commands;
using Domain.Events;
using SimpleCqrs.Domain;

namespace Domain
{
    public class Company : AggregateRoot
    {
        public Company(CreateComanyCommand create)
        {
            Apply(new CompanyAddedEvent { AggregateRootId = create.CompanyId,  Name = create.Name, Description = create.Description, Category = create.Category});
        }

        public Company()
        {
        }

        public void OnCompanyAdded(CompanyAddedEvent evnt)
        {
            Id = evnt.AggregateRootId;
        }



        public void Update(string newName, string newDescription, string newAddress)
        {
            Apply(new CompanyUpdatedEvent { AggregateRootId = Id, Name = newName, Address = newAddress, Description = newDescription });
        }




        public void AddReview(AddReviewToCompanyCommand command)
        {
            Apply(new CompantReviewAddedEvent
                           {
                               AggregateRootId = Id,
                               Date = DateTime.Now, 
                               IsGood = command.Type == AddReviewToCompanyCommand.ReviewType.Good, 
                               Text = command.Text
                           });
        }

    }
}