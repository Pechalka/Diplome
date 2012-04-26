using System.Collections.Generic;

namespace Diplom.Models
{
    public interface ICompanyRepository
    {
        Company GetBy(string id);
        List<Company> GetAll();
        string Save(Company company);
        List<Company> GetAllBy(string category);
    }
}