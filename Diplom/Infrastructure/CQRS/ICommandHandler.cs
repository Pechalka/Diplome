namespace Infrastructure.CQRS
{
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }
}
