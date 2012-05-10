using System;

namespace Domain.ViewModel
{
    public class CompanyDetailsViewModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public CompanyStatisticViewModel Navigation { get; set; }
    }
}