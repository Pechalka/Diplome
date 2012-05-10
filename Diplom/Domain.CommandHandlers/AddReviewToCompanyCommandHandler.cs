using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Commands;
using Infrastructure;
using Infrastructure.CQRS;
using Infrastructure.EventSourcing;

namespace Domain.CommandHandlers
{
    public class AddReviewToCompanyCommandHandler : ICommandHandler<AddReviewToCompanyCommand>
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly IPersistenceManager _persistenceManager;

        public AddReviewToCompanyCommandHandler(IRepository<Company> companyRepository, IPersistenceManager persistenceManager)
        {
            _companyRepository = companyRepository;
            _persistenceManager = persistenceManager;
        }

        public void Handle(AddReviewToCompanyCommand command)
        {
            var company = _companyRepository.ById(command.CompanyId);
            company.AddReview(command);

            _persistenceManager.Commit(); 
        }
    }
}
