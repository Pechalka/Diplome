using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.EventSourcing;
using MongoDB.Driver.Builders;

namespace Domain.PersistenceHandlers
{
    public class CompantReviewAddedEventHandler : IHandleEvent<CompantReviewAddedEvent>
    {
        public void Handle(CompantReviewAddedEvent evt)
        {
            var views = MongoHelper.GetCollectionOf<CompanyReviewsViewModel>();
            var companyView = views.FindOneById(evt.CompanyId);

            companyView.Reviews.Add(new CompanyReviewViewModel
                                        {
                                            IsGood = evt.IsGood, 
                                            Text = evt.Text, 
                                            Date = evt.Date.ToString(), 
                                            AvatarUrl = "http://placehold.it/160x120"
                                        });
            companyView.Navigation.ReviewCount++;

            views.Save(companyView);


            MongoHelper.GetCollectionOf<CompanyDetailsViewModel>().Update(Query.EQ("_id", evt.CompanyId),
                                                                          Update.Set("Navigation.ReviewCount",
                                                                                     companyView.Navigation.ReviewCount));
        }
    }
}
