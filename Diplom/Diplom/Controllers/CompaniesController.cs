using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain;
using Domain.Commands;
using Domain.ViewModel;
using Domain.ViewModel.Queries;
using Infrastructure.CQRS;

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
            form.Id = Guid.NewGuid();

            return Handle(form,
                RedirectToAction("Details", new { form.Id }), 
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


        public ActionResult Photos(Guid id)
        {
            var company = For<CompanyViewModel>().GetBy(id);
            return View(company);
        }

        [HttpGet]
        public ViewResult Change(Guid id)
        {
            var company = For<CompanyDetailsViewModel>().GetBy(id);

            var viewModel = new ChangeCompanyCommand
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                Address = company.Address
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Change(ChangeCompanyCommand form)
        {
            return Handle(form,
                          RedirectToAction("Details", new { form.Id }),
                          RedirectToAction("Change"));
        }


        public ActionResult List(string category = "", int page = 1)
        {
            return
                For<CompaniesListViewModel>()
                .Execute<IGetPageOfCompaniesQuery>()
                .WithParams(q =>
                {
                    q.Category = category;
                    q.Page = page;
                    q.PageSize = 10;
                });
        }


        public ActionResult Test()
        {
            return View();
        }
    }





}








