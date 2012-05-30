using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class AddWorkEventHandler : IHandleDomainEvents<AddWorkEvent>
    {
        public void Handle(AddWorkEvent domainEvent)
        {
            var companyWorkPage =   MongoHelper.GetCollectionOf<CompanyWorksPage>().FindOneById(domainEvent.AggregateRootId);
            companyWorkPage.Works.Add(new CompanyWork
                                          {
                                              WorkId = domainEvent.WorkId,
                                              Text = domainEvent.WorkText,
                                              Title = domainEvent.WorkTitle
                                          });
            MongoHelper.GetCollectionOf<CompanyWorksPage>().Save(companyWorkPage);
        }
    }
}
