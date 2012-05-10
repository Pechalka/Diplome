using Domain.Commands;
using Infrastructure;
using Infrastructure.CQRS;
using Infrastructure.EventSourcing;

namespace Domain.CommandHandlers
{
    public class ChangeCompanyCommandHandler : ICommandHandler<ChangeCompanyCommand>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IPersistenceManager _persistenceManager;

        public ChangeCompanyCommandHandler(IRepository<Company> companyRepository, IPersistenceManager persistenceManager)
        {
            _companyRepository = companyRepository;
            _persistenceManager = persistenceManager;
        }

        public void Handle(ChangeCompanyCommand command)
        {
            var company = _companyRepository.ById(command.Id);
            company.Update(command.Name, command.Description, command.Address);

            _persistenceManager.Commit();
        }
    }
}
