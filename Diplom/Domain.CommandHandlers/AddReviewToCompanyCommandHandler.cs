using Domain.Commands;
using SimpleCqrs.Commanding;
using SimpleCqrs.Domain;

namespace Domain.CommandHandlers
{
    public class AddReviewToCompanyCommandHandler : CommandHandler<AddReviewToCompanyCommand>
    {
        private readonly IDomainRepository _repository;

        public AddReviewToCompanyCommandHandler(IDomainRepository repository)
        {
            _repository = repository;
        }


        public override void Handle(AddReviewToCompanyCommand command)
        {
            var company = _repository.GetById<Company>(command.CompanyId);
            company.AddReview(command);

            _repository.Save(company);
        }
    }
}
