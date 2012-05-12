using Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Domain.CommandHandlers
{
    public class ChangeCompanyCommandHandler : CommandHandler<ChangeCompanyCommand>
    {
        private readonly IDomainRepository _repository;

        public ChangeCompanyCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }

        public override void Handle(ChangeCompanyCommand command)
        {
            var company = _repository.GetById<Company>(command.CompanyId);
            company.Update(command.Name, command.Description, command.Address);

            _repository.Save(company);
        }
    }
}
