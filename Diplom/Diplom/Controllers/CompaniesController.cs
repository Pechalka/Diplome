using System.Web.Mvc;
using Diplom.Models;
using Diplom.ViewModels;

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

        public ActionResult List(string category = "")
        {
            ViewData["category"] = category;
            return View(_companyRepository.GetAllBy(category));
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
