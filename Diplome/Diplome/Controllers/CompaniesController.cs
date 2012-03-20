using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Diplome.Models;

namespace Diplome.Controllers
{
    public class CompaniesController : ApiController
    {
        private readonly List<Company> companies = new List<Company>
                       {
                           new Company{ Id = 2, Name = "Надежда", Description = "Парихмахерская"},
                           new Company{ Id = 1, Name = "MMM", Description = "Финансовая перамида"}
                       };

        public IEnumerable<Company> Get()
        {
            return companies;
        }

        public Company Get(int id)
        {
            return companies.FirstOrDefault(c => c.Id == id);
        }
    }
}
