using Domain.Commands;
using Infrastructure.CQRS;

namespace Domain.CommandHandlers
{
    public class ChangeCompanyCommandHandler : ICommandHandler<ChangeCompanyCommand>
    {
        public void Handle(ChangeCompanyCommand command)
        {
            ICompanyRepository _companyRepository = new CompanyRepositoryMongo();
            var company = _companyRepository.GetBy(command.Id);

            company.Address = command.Address;
            company.Description = command.Description;
            company.Name = command.Name;

            _companyRepository.Save(company);
        }
    }
}
