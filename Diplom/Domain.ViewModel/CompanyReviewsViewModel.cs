using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Domain.ViewModel
{
    public class CompanyReviewsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CompanyReviewViewModel> Reviews { get; set; }

        public CompanyStatisticViewModel Navigation { get; set; }
    }
}
