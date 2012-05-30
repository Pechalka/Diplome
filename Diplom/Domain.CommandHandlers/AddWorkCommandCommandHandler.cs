using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Domain.CommandHandlers
{
    public class AddWorkCommandCommandHandler : CommandHandler<AddWorkCommand>
    {
        private readonly IDomainRepository _repository;

        public AddWorkCommandCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }


        public override void Handle(AddWorkCommand command)
        {
            var company = _repository.GetById<Company>(command.CompanyId);
            company.AddWork(command);
            _repository.Save(company);
        }
    }
}
