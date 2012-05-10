using System;
using Infrastructure.CQRS;

namespace Domain.Commands
{
    public class AddReviewToCompanyCommand : ICommand
    {
        public enum ReviewType
        {
            Undefined,
            Good,
            Bad
        }
        public Guid CompanyId { get; set; }
        public ReviewType Type { get; set; }
        public string Text { get; set; }
    }
}
