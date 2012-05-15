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


        public override void Handle(CreateComanyCommand create)
        {
            var company = new Company(create);
            _repository.Save(company);
        }
    }
}
