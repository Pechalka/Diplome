using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using MongoDB.Driver.Builders;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class CompantReviewAddedEventHandler : IHandleDomainEvents<CompantReviewAddedEvent>
    {
        public void Handle(CompantReviewAddedEvent evt)
        {
            var views = MongoHelper.GetCollectionOf<ReviewCompanyDetailsViewModel>();
            var companyView = views.FindOneById(evt.AggregateRootId);

            companyView.Reviews.Add(new CompanyReviewViewModel
                                        {
                                            IsGood = evt.IsGood, 
                                            Text = evt.Text, 
                                            Date = evt.Date.ToString("dd.MM.yyyy"), 
                                            AvatarUrl = "http://placehold.it/160x120"
                                        });
          //  companyView.Navigation.ReviewCount++;

            views.Save(companyView);


           // MongoHelper.GetCollectionOf<CompanyDetailsViewModel>().Update(Query.EQ("_id", evt.AggregateRootId),
             //                                                             Update.Set("Navigation.ReviewCount",
              //                                                                       companyView.Navigation.ReviewCount));
        }
    }
}
