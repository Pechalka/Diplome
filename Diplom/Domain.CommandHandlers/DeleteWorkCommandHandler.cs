using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Domain.CommandHandlers 
{
    public class DeleteWorkCommandHandler : CommandHandler<DeleteWorkCommand>
    {
        private readonly IDomainRepository _repository;

        public DeleteWorkCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(DeleteWorkCommand command)
        {
            var company = _repository.GetById<Company>(command.CompanyId);
            company.DeleteWork(command);
            _repository.Save(company);
        }
    }
}
