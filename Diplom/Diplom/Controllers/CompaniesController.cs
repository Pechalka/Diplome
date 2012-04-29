using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Diplom.Models;
using Diplom.ViewModels;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Diplom.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public CompaniesController()
            : this(new CompanyRepositoryMongo())
        {
        }

        public ActionResult Details(string id)
        {
            return View(_companyRepository.GetBy(id));
        }

        public ActionResult List(string category = "", int page = 1)
        {
            var viewModel = GetPageOfCompaniesQuery.Execute(category, page);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateComanyViewModel form)
        {
            var id = _companyRepository.Save(new Company
                                                 {
                                                     Category = form.Category,
                                                     Name = form.Name,
                                                     Description = form.Description
                                                 });

            return RedirectToAction("Details", new {id});
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateComanyViewModel());
        }

        [HttpPost]
        public ActionResult Change(ChangeCompanyViewModel form)
        {
            var company = _companyRepository.GetBy(form.Id);

            company.Address = form.Address;
            company.Description = form.Description;
            company.Name = form.Name;

            _companyRepository.Save(company);

            return RedirectToAction("Details", new {form.Id});
        }

        [HttpGet]
        public ViewResult Change(string id)
        {
            var company = _companyRepository.GetBy(id);

            var viewModel = new ChangeCompanyViewModel
                                {
                                    Id = company.Id,
                                    Name = company.Name,
                                    Description = company.Description,
                                    Address = company.Address
                                };

            return View(viewModel);
        }


        public ActionResult Test()
        {
            return View();
        }
    }
}


public class CompaniesListViewModel
{
    public string Category { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public List<Company> Companies { get; set; }
}

public class GetPageOfCompaniesQuery
{
    public static CompaniesListViewModel Execute(string category, int page, int pageSize = 5)
    {
        IMongoQuery query = null;   // all
        if (!string.IsNullOrEmpty(category))
            query = Query.EQ("Category", category.ToUpper());

        int totalPages;
        var companies = MongoHelper.GetCollectionOf<Company>().GetPage(query, page, pageSize,
                                                    out totalPages);


        return new CompaniesListViewModel
                   {
                       Category = category,
                       CurrentPage = page,
                       TotalPages = totalPages,
                       Companies = companies
                   };
    }


}


public static class MongoHelper
{
    public static List<T> GetPage<T>(this MongoCollection<T> collection, IMongoQuery query, int page, int pageSize, out int totalPages)
    {
        var getCountCursor = new MongoCursor<T>(collection, query);
        var mainCountCursor = new MongoCursor<T>(collection, query);

        long countItems = getCountCursor.Count();
        totalPages = (int)Math.Ceiling((double)countItems / pageSize);

        return mainCountCursor
            .SetSkip((page - 1) * pageSize)
            .SetLimit(pageSize)
            .ToList();
    }

    public static MongoCollection<T> GetCollectionOf<T>()
    {
        var server = MongoServer.Create();
        var database = server.GetDatabase("Diplome");

         return database.GetCollection<T>("companies");
    }
}