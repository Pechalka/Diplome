namespace Infrastructure.CQRS
{
    public interface IQuery<TResult>
    {
        TResult Execute();
    }
}