using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace Raika.Common.SharedKernel.Interfaces
{
    public interface IQueryGenericRepository<T, EntityKey> where T : class
    {
        //
        // Find by key
        //
        Task<T> FindByKeyAsync(EntityKey Id, string entityKeyName = "Id");        
        Task<T> FindByKeyAsync(EntityKey Id, CancellationToken cancellationToken, string entityKeyName = "Id");

        //
        // Exist
        //
        Task<bool> ExistAsync(EntityKey Id, string entityKeyName = "Id");
        Task<bool> ExistAsync(EntityKey Id, CancellationToken cancellationToken, string entityKeyName = "Id");

        //
        // Find by condition
        //
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted");
        Task<IEnumerable<TResult>> FindByConditionAsync<TResult>(Expression<Func<T, bool>> predicate, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted") where TResult : class;

        //
        // Get All
        //
        Task<IEnumerable<T>> GetAllAsync(bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted");
        Task<IEnumerable<T>> GetAllAsync(bool showDeletedRecords, string isDeletedFieldName, CancellationToken cancellationToken);
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(string entityName, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted" ) where TResult : class;
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(string entityName, bool showDeletedRecords, string isDeletedFieldName, CancellationToken cancellationToken) where TResult : class;
        
        //
        // Custom query
        //
        Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters parms, CommandType commandType = CommandType.Text);
        Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters parms, CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default);
    }
}
