using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Diplom.Models
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

        public Company GetBy(string id)
        {
            return companies.FindOneById(ObjectId.Parse(id));
        }

        public List<Company> GetAll()
        {
            return companies.FindAll().ToList();
        }

        public string Save(Company company)
        {
            
            companies.Save(company);

            return company.Id;
        }
    }
}