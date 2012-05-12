using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class CompaniesListViewModel 
    {
        public string Category { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<CompanyViewModel> Companies { get; set; }
    }
}
