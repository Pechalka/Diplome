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

        public CompaniesController():this(new CompanyRepositoryFake())
        {
        }

        public ActionResult Details(int id)
        {
            return View(_companyRepository.GetBy(id));
        }

        public ActionResult List()
        {
            return View(_companyRepository.GetAll());
        }

        [HttpPost]
        public ActionResult Create(CreateComanyViewModel form)
        {
            var id = _companyRepository.Save(Company.Create(form.Name, form.Description));

            return RedirectToAction("Details", new { id });
        }

        [HttpGet]
         public ActionResult Create()
        {
            return View(new CreateComanyViewModel());
        }
    }
}
