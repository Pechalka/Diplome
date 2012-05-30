using System;
using SimpleCqrs.Commanding;

namespace Domain.Commands
{
    public class AddWorkCommand : ICommand
    {
        public Guid CompanyId { get; set; }

        public Guid WorkId { get; set; }
        public string WorkText { get; set; }
        public string WorkTitle { get; set; }
    }
}