using Domain.Commands;
using Infrastructure.CQRS;
using Infrastructure.EventSourcing;

namespace Domain.CommandHandlers
{
    public class CreateComanyCommandHandler : ICommandHandler<CreateComanyCommand>
    {
        public void Handle(CreateComanyCommand command)
        {
            //ICompanyRepository _companyRepository = new CompanyRepositoryMongo();
            //_companyRepository.Save(new Company
            //                                     {
            //                                         Id = command.Id,
            //                                         Category = command.Category,
            //                                         Name = command.Name,
            //                                         Description = command.Description
            //                                     });

            var rep = new EventSourcedRepository<Company>(new MongoEventStore());
            rep.Add(new Company(command.Id, command.Name ));
        }
    }
}
