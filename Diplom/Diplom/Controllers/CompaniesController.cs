using System;
using System.Web.Mvc;
using Diplom.HtmlHalpers;
using Diplom.Models;
using Diplom.ViewModels;
using Domain.Commands;
using Domain.ViewModel;
using Infrastructure;
using Infrastructure.CQRS;
using MongoDB.Driver.Builders;

namespace Diplom.Controllers
{

    public class CompaniesController : CQRSController
    {
        private readonly IAuthProvider _authProvider;

        public CompaniesController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ActionResult Details(Guid id)
        {
            var form = For<DetailsViewModel>().GetBy(id);

            form.SetPermission(_authProvider.CurrentUserId);
            return View(form);
        }

        public ActionResult Photos(Guid id)
        {
            var company = For<PhotosCompanyDetailsViewModel>().GetBy(id);
            company.SetPermission(_authProvider.CurrentUserId);

            return View(company);
        }

        [HttpGet]
        public ActionResult Reviews(Guid id)
        {
            var form = For<ReviewCompanyDetailsViewModel>().GetBy(id);
            form.SetPermission(_authProvider.CurrentUserId);

            return View(form);
        }

        public ActionResult Work(Guid id)
        {
            var form = For<CompanyWorksPage>().GetBy(id);
            form.SetPermission(_authProvider.CurrentUserId);

            return View(form);
        }

        [HttpGet]
        public ActionResult WorkEdit(Guid id)
        {
            var form = For<CompanyWorksPage>().GetBy(id);
            form.SetPermission(_authProvider.CurrentUserId);

            return View(form);
        }

        [HttpPost]
        public ActionResult WorkEdit(Guid companyId, CompanyWork form)
        {
            var command = new AddWorkCommand
                              {
                                  CompanyId = companyId,
                                  WorkId = form.WorkId,
                                  WorkText = form.Text,
                                  WorkTitle = form.Title
                              };

            return Handle(command,
                          RedirectToAction("WorkEdit", new {id = companyId}),
                          RedirectToAction("WorkEdit", new {id = companyId}));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CreateComanyCommand
                            {
                                OwnerUserId = _authProvider.CurrentUserId
                            });
        }


        [HttpPost]
        public ActionResult Create(CreateComanyCommand form)
        {
            form.CompanyId = Guid.NewGuid();

            return Handle(form,
                RedirectToAction("Details", new { id = form.CompanyId }),
                RedirectToAction("Create"));
        }

        [HttpPost]
        public ActionResult Reviews(AddReviewToCompanyCommand form)
        {
            var redirect = RedirectToAction("Reviews", new {id = form.CompanyId});

            return Handle(form, redirect, redirect);
        }

        [HttpGet]
        public ActionResult Change(Guid id)
        {
            var form = For<DetailsViewModel>().GetBy(id);
            form.SetPermission(_authProvider.CurrentUserId);

            return View(form);
        }


        [HttpPost]
        public ActionResult Change(ChangeCompanyCommand form)
        {
            return Handle(form,
                          RedirectToAction("Details", new { id = form.CompanyId }),
                          RedirectToAction("Change"));
        }

        public int PageSize = 3;

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



        public ActionResult Test()
        {
            return View();
        }

        public ActionResult DeleteWork(Guid companyId, Guid workId)
        {
            var command = new DeleteWorkCommand
                              {
                                  CompanyId = companyId,
                                  WorkId = workId
                              };

            return Handle(command,
                          RedirectToAction("WorkEdit", new { id = companyId }),
                          RedirectToAction("WorkEdit", new { id = companyId }));
        }
    }







}





