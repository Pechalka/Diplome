using System.Collections.Generic;
using Diplom.HtmlHalpers;
using Domain.ViewModel;

namespace Diplom.ViewModels
{
    public class CompaniesListViewModel 
    {
        public string Category { get; set; }

        public List<CompanyViewModel> Companies { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
