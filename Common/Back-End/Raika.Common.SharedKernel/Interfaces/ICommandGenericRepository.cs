namespace Raika.Common.SharedKernel.Interfaces
{
    public interface ICommandGenericRepository<T> where T : class, IAggregateRoot
    {
        Task SoftDeleteAsync(T entity);
        Task SoftDeleteAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<int> ExecuteSqlSpAsync(string sqlSp);
        Task<int> ExecuteSqlSpAsync(string sqlSp, CancellationToken cancellationToken);
        Task<int> SaveChangeAsync();
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);
    }
}
