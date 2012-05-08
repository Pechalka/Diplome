using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.EventSourcing;

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

            var companyDetails = MongoHelper.GetCollectionOf<CompanyDetailsViewModel>();
            companyDetails.Save(new CompanyDetailsViewModel
            {
                Id = evt.Id,
                Name = evt.Name,
                Description = evt.Description,
                Address = evt.Address
            });
        }
    }
}
