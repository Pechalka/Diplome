using Domain.Commands;
using Infrastructure;
using Infrastructure.CQRS;
using Infrastructure.EventSourcing;

namespace Domain.CommandHandlers
{
    public class CreateComanyCommandHandler : ICommandHandler<CreateComanyCommand>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IPersistenceManager _persistenceManager;

        public CreateComanyCommandHandler(IRepository<Company> companyRepository, IPersistenceManager persistenceManager)
        {
            _companyRepository = companyRepository;
            _persistenceManager = persistenceManager;
        }

        public void Handle(CreateComanyCommand command)
        {
            _companyRepository.Add(new Company(command.Id, command.Name, command.Description));
            _persistenceManager.Commit();
        }
    }
}
