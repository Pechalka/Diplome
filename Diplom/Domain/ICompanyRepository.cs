using System;
using System.Collections.Generic;

namespace Domain
{
    public interface ICompanyRepository
    {
        Company GetBy(Guid id);
        List<Company> GetAll();
        void Save(Company company);
        List<Company> GetAllBy(string category);
    }
}