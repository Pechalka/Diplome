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
            return View(For<Company>().GetBy(id));
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






        public ActionResult Reviews(Guid id)
        {
            var company = For<Company>().GetBy(id);
            return View(new CompanyViewModel
            {
                Name = company.Name,
                Id = company.Id.ToString()
            });
        }

        public ActionResult Photos(Guid id)
        {
            var company = For<Company>().GetBy(id);
            return View(new CompanyViewModel
                            {
                                Name = company.Name,
                                Id = company.Id.ToString()
                            });
        }

        [HttpGet]
        public ViewResult Change(Guid id)
        {
            var company = For<Company>().GetBy(id);

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
            var q = new GetPageOfCompaniesQuery();
            q.Category = category;
            q.Page = page;
            q.PageSize = 10;

            return View(q.Execute());
            //return
            //    For<CompaniesListViewModel>()
            //    .Execute<IGetPageOfCompaniesQuery>()
            //    .WithParams(q =>
            //    {
            //        q.Category = category;
            //        q.Page = page;
            //        q.PageSize = 10;
            //    });
        }
    }


    public class CompanyReviewsViewModel
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public List<CompanyReviewViewModel> Reviews { get; set; }
    }

    public class CompanyReviewViewModel
    {
        public string AvatarUrl { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public bool IsGood { get; set; }
    }
}








