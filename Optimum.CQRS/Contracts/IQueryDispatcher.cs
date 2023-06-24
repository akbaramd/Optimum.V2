namespace Optimum.CQRS.Contracts;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TQuery,TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult> where TResult : class;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default) where TResult : class;
}