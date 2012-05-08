using System;
using System.Collections.Generic;

namespace Domain.ViewModel
{
    public class CompanyReviewsViewModel
    {
        public Guid CompanyId { get; set; }
        public string Name { get; set; }
        public List<CompanyReviewViewModel> Reviews { get; set; }
    }
}
