using System;

namespace Domain.ViewModel
{
    public class CompanyStatisticViewModel
    {

        public Guid CompanyId { get; set; }
        public int ReviewCount { get; set; }
        public string Selected { get; set; }
    }
}
