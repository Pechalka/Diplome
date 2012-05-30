using System;
using Domain.Commands;
using Domain.Events;
using SimpleCqrs.Domain;
using SimpleCqrs.Eventing;

namespace Domain
{
    public class Company : AggregateRoot
    {
        public Company(CreateComanyCommand create)
        {
            Apply(new CompanyAddedEvent { AggregateRootId = create.CompanyId, UserId  =  create.OwnerUserId, Name = create.Name, Description = create.Description, Category = create.Category});
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

        public void AddWork(AddWorkCommand command)
        {
            var evnt = new AddWorkEvent
                           {
                               AggregateRootId = command.CompanyId,
                               WorkId = command.WorkId,
                               WorkText = command.WorkText,
                               WorkTitle = command.WorkTitle
                           };
            Apply(evnt);
        }

        public void DeleteWork(DeleteWorkCommand command)
        {
            var evnt = new WorkDeletedEvent
                          {
                              AggregateRootId = command.CompanyId,
                              WorkId = command.WorkId
                          };
            Apply(evnt);
        }
    }
}

