using Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Domain.CommandHandlers
{
    public class CreateComanyCommandHandler : CommandHandler<CreateComanyCommand>
    {
        private readonly IDomainRepository _repository;

        public CreateComanyCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }


        public override void Handle(CreateComanyCommand command)
        {
            var company = new Company(command.CompanyId, command.Name, command.Description);
            _repository.Save(company);
        }
    }
}
