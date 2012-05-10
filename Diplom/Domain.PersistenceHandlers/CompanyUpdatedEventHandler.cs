using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.EventSourcing;
using MongoDB.Driver.Builders;

namespace Domain.PersistenceHandlers
{
    public class CompanyUpdatedEventHandler : IHandleEvent<CompanyUpdatedEvent>
    {
        public void Handle(CompanyUpdatedEvent evt)
        {
            var companyItems = MongoHelper.GetCollectionOf<CompanyViewModel>();
            companyItems.Save(new CompanyViewModel
            {
                Id = evt.Id,
                Name = evt.Name,
                Description = evt.Description
            });

            var update = Update.Set("Name", evt.Name).Set("Description", evt.Description).Set("Address", evt.Address);

            MongoHelper.GetCollectionOf<CompanyDetailsViewModel>()
                .Update(Query.EQ("_id", evt.Id), update);


        }
    }
}
