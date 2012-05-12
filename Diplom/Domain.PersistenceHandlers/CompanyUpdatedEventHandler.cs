using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using MongoDB.Driver.Builders;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class CompanyUpdatedEventHandler : IHandleDomainEvents<CompanyUpdatedEvent>
    {
        public void Handle(CompanyUpdatedEvent evt)
        {
            var companyItems = MongoHelper.GetCollectionOf<CompanyViewModel>();
            companyItems.Save(new CompanyViewModel
            {
                Id = evt.AggregateRootId,
                Name = evt.Name,
                Description = evt.Description
            });

            var update = Update.Set("Name", evt.Name).Set("Description", evt.Description).Set("Address", evt.Address);

            MongoHelper.GetCollectionOf<CompanyDetailsViewModel>()
                .Update(Query.EQ("_id", evt.AggregateRootId), update);


        }
    }
}
