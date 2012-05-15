using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Diplom.HtmlHalpers;
using Diplom.ViewModels;
using Domain;
using Domain.Commands;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.CQRS;
using MongoDB.Driver.Builders;

namespace Diplom.Controllers
{
    public class CompaniesController : CQRSController
    {
        public ActionResult Details(Guid id)
        {
            var form = For<CompanyDetailsViewModel>().GetBy(id);
            form.Navigation.Selected = "1";
            return View(form);
        }

        [HttpPost]
        public ActionResult Create(CreateComanyCommand form)
        {
            form.CompanyId = Guid.NewGuid();

            return Handle(form,
                RedirectToAction("Details", new { id = form.CompanyId }), 
                RedirectToAction("Create"));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateComanyCommand());
        }





        [HttpGet]
        public ActionResult Reviews(Guid id)
        {
            var form = For<CompanyReviewsViewModel>().GetBy(id);
            form.Navigation.Selected = "2";
            return View(form);
        }

        [HttpPost]
        public ActionResult Reviews(AddReviewToCompanyCommand form)
        {
            var redirect = RedirectToAction("Reviews", new {id = form.CompanyId});

            return Handle(form, redirect, redirect);
        }


        public ActionResult Photos()
        {
          //  var company = For<CompanyViewModel>().GetBy(id);
           // return View(company);
            return View();
        }

        [HttpGet]
        public ViewResult Change(Guid id)
        {
            var viewModel = For<CompanyDetailsViewModel>().GetBy(id);
            viewModel.Navigation.Selected = "1";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Change(ChangeCompanyCommand form)
        {
            return Handle(form,
                          RedirectToAction("Details", new { id = form.CompanyId }),
                          RedirectToAction("Change"));
        }

        public int PageSize = 12;

        public ActionResult List(string category = "", int page = 1)
        {
            long totalItem;
            var query = !string.IsNullOrEmpty(category) ? Query.EQ("Category", category.ToUpper()) : null;
            var companies = Collection<CompanyViewModel>().GetPage(query, page, PageSize, out totalItem);
            var pageInfo = new PagingInfo
                               {
                                   CurrentPage = page,
                                   ItemsPerPage = PageSize,
                                   TotalItem = (int)totalItem
                               };         

            return View(new CompaniesListViewModel
            {
                Companies = companies,
                PagingInfo = pageInfo,
                Category = category
            });
        }

        public ActionResult Services(Guid id)
        {
            var viewModel = For<CompanyDetailsViewModel>().GetBy(id);
            viewModel.Navigation.Selected = "3";

            return View(viewModel);
        }


        public ActionResult Test()
        {
            return View();
        }
    }



    public class ServiceViewModel
    {
        public string Price { get; set; }
        public bool HasPrice
        {
            get { return string.IsNullOrEmpty(Price); }
        }
        public string Decscription { get; set; }
        public bool HasDecscription
        {
            get { return string.IsNullOrEmpty(Decscription); }
        }


        public string Title { get; set; }
        
        
        public bool ReadyNow { get; set; }
        
    }

}








