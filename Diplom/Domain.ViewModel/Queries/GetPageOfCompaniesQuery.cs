using Infrastructure;
using Infrastructure.CQRS;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Domain.ViewModel.Queries
{
    //public class GetPageOfCompaniesQuery : IGetPageOfCompaniesQuery
    //{
    //    public CompaniesListViewModel Execute()
    //    {
    //        IMongoQuery query = null;   // all
    //        if (!string.IsNullOrEmpty(Category))
    //            query = Query.EQ("Category", Category.ToUpper());

    //        int totalPages;
  //          var companies = MongoHelper.GetCollectionOf<CompanyViewModel>().GetPage(query, Page, PageSize,
  //                                                      out totalPages);


    //        return new CompaniesListViewModel
    //        {
    //            Category = Category,
    //            CurrentPage = Page,
    //            TotalPages = totalPages,
    //            Companies = companies
    //        };
    //    }

    //    public string Category { get; set; }
    //    public int Page { get; set; }
    //    public int PageSize { get; set; }
    //}

    //public interface IGetPageOfCompaniesQuery : IQuery<CompaniesListViewModel>
    //{
    //    string Category { get; set; }
    //    int Page { get; set; }
    //    int PageSize { get; set; }
    //}
}
