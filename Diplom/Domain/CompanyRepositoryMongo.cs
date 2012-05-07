using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Domain
{
    public class CompanyRepositoryMongo : ICompanyRepository
    {
        private MongoCollection<Company> companies;

        public CompanyRepositoryMongo()
        {

            var server = MongoServer.Create();
            var database = server.GetDatabase("Diplome");
            companies = database.GetCollection<Company>("companies");
        }

        public Company GetBy(Guid id)
        {
            return companies.FindOneById(id);
        }

        public List<Company> GetAll()
        {
            return companies.FindAll().ToList();
        }

        public void Save(Company company)
        {
            company.Category = company.Category.ToUpper();
            companies.Save(company);
        }

        public List<Company> GetAllBy(string category)
        {
            if (string.IsNullOrEmpty(category))
                return GetAll();

            category = category.ToUpper();
            return companies.Find(Query.EQ("Category", category)).ToList();
        }
    }
}