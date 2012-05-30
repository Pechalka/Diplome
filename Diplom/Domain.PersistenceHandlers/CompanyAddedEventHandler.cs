using System.Collections.Generic;
using Domain.Events;
using Domain.ViewModel;
using Infrastructure;
using SimpleCqrs.Eventing;

namespace Domain.PersistenceHandlers
{
    public class CompanyAddedEventHandler : IHandleDomainEvents<CompanyAddedEvent>
    {

        private void addServices(CompanyDetailsBase company)
        {
           // company.Services = new List<string>();

            //for (int i = 0; i < 9; i++)
            //{
            //    company.Services.Add("Услуга");
            //}
        }

        private void createMainPage(CompanyAddedEvent @event)
        {
            var companyItems = MongoHelper.GetCollectionOf<CompanyViewModel>();
            companyItems.Save(new CompanyViewModel
            {
                Id = @event.AggregateRootId,
                Name = @event.Name,
                Description = @event.Description,
                Category = @event.Category
            });

            var companyDetails = MongoHelper.GetCollectionOf<DetailsViewModel>();

            var company = new DetailsViewModel
                              {
                                  CompanyId = @event.AggregateRootId,
                                  CompanyName = @event.Name,
                                  CompanyDescription = @event.Description,
                                  CompanyAddress = "",
                                  News = new List<string>(),
                                  CreatedBy = @event.UserId
                              };
            for (int n = 0; n <= 5; n++)
            {
                company.News.Add(@"Полноценным тест-драйвом это назвать нельзя. Можно сказать, встреча друзей. Просто когда у менеджера Opel по восточной Европе Андраша Сальго предоставилась возможность, он пригласил нас в Ригу прокатиться на новом фургоне Opel Combo и про
");
            }

            addServices(company);

            companyDetails.Save(company);
        }

        private void createPhotosPage(CompanyAddedEvent @event)
        {

            var photosDetails = MongoHelper.GetCollectionOf<PhotosCompanyDetailsViewModel>();

            var company = new PhotosCompanyDetailsViewModel
                              {
                                //  Services = new List<string>(),
                                  CompanyId = @event.AggregateRootId,
                                  CompanyName = @event.Name,
                                  Photos = new List<string> { "1", "2", "3", "4", "5", "6", },
                                  CreatedBy = @event.UserId
                              };

            addServices(company);

            photosDetails.Save(company);
        }

        private void createReviewPage(CompanyAddedEvent @event)
        {
            var companies = MongoHelper.GetCollectionOf<ReviewCompanyDetailsViewModel>();

            var comapny = new ReviewCompanyDetailsViewModel
                                {
                                   // Services = new List<string>(),
                                    CompanyId = @event.AggregateRootId,
                                    CompanyName = @event.Name,
                                    Reviews = new List<CompanyReviewViewModel>(),
                                    CreatedBy = @event.UserId
                                };

            addServices(comapny);

            companies.Save(comapny);
        }

        public void createWorkPage(CompanyAddedEvent @event)
        {

            var companies = MongoHelper.GetCollectionOf<CompanyWorksPage>();

            var comapny = new CompanyWorksPage
            {
             //   Services = new List<string>(),
                CompanyId = @event.AggregateRootId,
                CompanyName = @event.Name,
                Works = new List<CompanyWork>(),
                CreatedBy = @event.UserId
            };

            addServices(comapny);

            companies.Save(comapny);


        }


        public void Handle(CompanyAddedEvent @event)
        {
            createMainPage(@event);
            createReviewPage(@event);
            createWorkPage(@event);
            createPhotosPage(@event);
        }


    }
}
