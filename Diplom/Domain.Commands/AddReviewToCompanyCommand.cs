using System;
using SimpleCqrs.Commanding;

namespace Domain.Commands
{
    public class AddReviewToCompanyCommand :  ICommand
    {
        public enum ReviewType
        {
            Undefined,
            Good,
            Bad
        }
        public ReviewType Type { get; set; }
        public string Text { get; set; }
        public Guid CompanyId { get; set; }
    }
}
