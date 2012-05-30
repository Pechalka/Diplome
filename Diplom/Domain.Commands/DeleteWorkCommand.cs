using System;
using SimpleCqrs.Commanding;

namespace Domain.Commands
{
    public class DeleteWorkCommand : ICommand
    {
        public Guid WorkId { get; set; }
        public Guid CompanyId { get; set; }
    }
}