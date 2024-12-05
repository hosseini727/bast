using Microsoft.EntityFrameworkCore;
using Raika.Common.SharedKernel.Interfaces;

namespace Raika.HomeAlarmPanel.Infrastructure.RepositoryBase
{
    public class CommandRepositoryBase<T> : ICommandGenericRepository<T> where T : class, IAggregateRoot
    {
        private readonly DbContext _dbContext;
        public CommandRepositoryBase(DbContext dbContext) => _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            return await AddAsync(entity, CancellationToken.None);
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task SoftDeleteAsync(T entity)
        {
            await SoftDeleteAsync(entity, CancellationToken.None);
        }
        public async Task SoftDeleteAsync(T entity, CancellationToken cancellationToken)
        {
            //_dbContext.Set<T>().Entry(entity).State = EntityState.Deleted;
            _dbContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<int> ExecuteSqlSpAsync(string sqlSp)
        {
            return await ExecuteSqlSpAsync(sqlSp, CancellationToken.None);
        }
        public async Task<int> ExecuteSqlSpAsync(string sqlSp, CancellationToken cancellationToken)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync($"{sqlSp}", cancellationToken);
        }

        public async Task UpdateAsync(T entity)
        {
            await UpdateAsync(entity, CancellationToken.None);
        }
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await SaveChangeAsync(CancellationToken.None);
        }
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
