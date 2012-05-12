using System;
using SimpleCqrs.Commanding;

namespace Domain.Commands
{
    public class ChangeCompanyCommand : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid CompanyId { get; set; }
    }
}
