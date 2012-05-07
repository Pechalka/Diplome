using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class CompanyRepositoryEventSourcing : ICompanyRepository
    {
        public Company GetBy(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Company> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(Company company)
        {

        }

        public List<Company> GetAllBy(string category)
        {
            throw new NotImplementedException();
        }
    }
}
