using System.Collections.Generic;
using System.Web.Http;
using Diplome.Models;

namespace Diplome.Controllers
{
    public class CompaniesController : ApiController
    {
        public IEnumerable<Company> Get()
        {
            return new List<Company>
                       {
                           new Company{ Id = 2, Name = "Надежда", Description = "Парихмахерская"},
                           new Company{ Id = 1, Name = "MMM", Description = "Финансовая перамида"}
                       };
        }
    }
}
