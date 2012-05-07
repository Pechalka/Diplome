using System;
using Infrastructure.CQRS;

namespace Domain.Commands
{
    public class ChangeCompanyCommand : ICommand
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public Guid Id { get; set; }
    }
}
