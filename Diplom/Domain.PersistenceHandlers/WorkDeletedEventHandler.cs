using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class WorkDeletedEventHandler : IHandleDomainEvents<WorkDeletedEvent>
    {
        public void Handle(WorkDeletedEvent domainEvent)
        {
            var companyWorkPage = MongoHelper.GetCollectionOf<CompanyWorksPage>().FindOneById(domainEvent.AggregateRootId);

            var deletedWork = companyWorkPage.Works.Find(w => w.WorkId == domainEvent.WorkId);
            companyWorkPage.Works.Remove(deletedWork);

            MongoHelper.GetCollectionOf<CompanyWorksPage>().Save(companyWorkPage);
        }
    }
}
