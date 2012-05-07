using System.Collections.Generic;
using Infrastructure.EventSourcing;

namespace Domain.ViewModel
{
    public class CompaniesListViewModel 
    {
        public string Category { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<Company> Companies { get; set; }
    }
}
