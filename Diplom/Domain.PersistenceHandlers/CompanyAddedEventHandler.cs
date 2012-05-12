using System.Collections.Generic;
using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class CompanyAddedEventHandler : IHandleDomainEvents<CompanyAddedEvent>
    {
        public void Handle(CompanyAddedEvent @event)
        {
            var companyItems = MongoHelper.GetCollectionOf<CompanyViewModel>();
            companyItems.Save(new CompanyViewModel
                                {
                                    Id = @event.AggregateRootId,
                                    Name = @event.Name,
                                    Description = @event.Description
                                });

            var companyDetails = MongoHelper.GetCollectionOf<CompanyDetailsViewModel>();
            companyDetails.Save(new CompanyDetailsViewModel
                                {
                                    Id = @event.AggregateRootId,
                                    Name = @event.Name,
                                    Description = @event.Description,
                                    Address = "",
                                    Navigation = new CompanyStatisticViewModel
                                                     {
                                                         CompanyId = @event.CompanyId,
                                                         ReviewCount = 0
                                                     }
                                });

            var reviewDetails = MongoHelper.GetCollectionOf<CompanyReviewsViewModel>();
            reviewDetails.Save(new CompanyReviewsViewModel
            {
                Id = @event.AggregateRootId,
                Name = @event.Name,
                Reviews = new List<CompanyReviewViewModel>(),
                Navigation = new CompanyStatisticViewModel
                {
                    CompanyId = @event.CompanyId,
                    ReviewCount = 0
                }
            });
        }
    }
}
