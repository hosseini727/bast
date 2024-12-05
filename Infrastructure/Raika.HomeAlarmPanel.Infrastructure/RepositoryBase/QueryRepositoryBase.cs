using Dapper;
using Raika.Common.SharedApplicationServices.Common;
using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.Infrastructure.DbContexts;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace Raika.HomeAlarmPanel.Infrastructure.RepositoryBase
{
    public class QueryRepositoryBase<T, EntityKey> : IQueryGenericRepository<T, EntityKey> where T : class, IAggregateRoot
    {
        private readonly QueryDbContext _queryDbContext;
        public QueryRepositoryBase(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }

        //
        // ExecuteCustomQueryAsync
        //
        public async Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters? parms = null,
            CommandType commandType = CommandType.Text)
        {
            return await ExecuteCustomQueryAsync(command, parms, commandType, CancellationToken.None);
        }

        public async Task<IQueryable<T>> ExecuteCustomQueryAsync(string command, DynamicParameters? parms = null,
            CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            using (var connection = _queryDbContext.CreateConnection())
            {
                connection.Open();
                IQueryable<T> result = (IQueryable<T>)await Task.Run(() => connection
                    .QueryAsync(command, parms, null, null, commandType), cancellationToken);
                connection.Close();
                return result;
            }
        }

        //
        // ExistAsync
        //
        public async Task<bool> ExistAsync(EntityKey Id, string entityKeyName)
        {
            return await ExistAsync(Id, CancellationToken.None, entityKeyName);
        }
        public async Task<bool> ExistAsync(EntityKey Id, CancellationToken cancellationToken, string entityKeyName)
        {
            string entityName = typeof(T).Name;
            string query = $"Select [{entityKeyName}] From [{entityName}] where [{entityName}].[{entityKeyName}] = @Id";
            using (var connection = _queryDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = Id });
                connection.Close();
                return result is not null;
            }
        }

        //
        // FindByKeyAsync
        //
        public async Task<T> FindByKeyAsync(EntityKey Id, string entityKeyName)
        {
            return await FindByKeyAsync(Id, CancellationToken.None, entityKeyName);
        }
        public async Task<T> FindByKeyAsync(EntityKey Id, CancellationToken cancellationToken, string entityKeyName)
        {
            string entityName = typeof(T).Name;
            string idName = entityName;
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(T).GetProperties();
            List<string> formattedColums = new();
            foreach (var property in propertyInfos)
                formattedColums.Add($"[{property}]");
            //string query = $"SELECT {string.Join(",", formattedColums)} FROM [{entityName}] WHERE [{entityName}].[{entityKeyName}] = @Id";            
            string query = $"SELECT * FROM [{entityName}] WHERE [{entityName}].[{entityKeyName}] = @Id";
            using (var connection = _queryDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = Id });
                connection.Close();
                return result;
            }
        }

        //
        // Find by condition
        //
        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted")
        {
            using (var connection = _queryDbContext.CreateConnection())
            {
                string entityName = typeof(T).Name;
                PredicateExpressionVisitor<T> predicateExpressionVisitor = new PredicateExpressionVisitor<T>();
                var condition = predicateExpressionVisitor.ConvertToSql(predicate);
                string query = showDeletedRecords
                    ? $"SELECT * FROM [{entityName}] WHERE {condition}"
                    : $"SELECT * FROM [{entityName}] WHERE {condition} AND {isDeletedFieldName} = 0";
                connection.Open();
                var result = await connection.QueryAsync<T>(query);
                connection.Close();
                return result;
            }
        }
        public async Task<IEnumerable<TResult>> FindByConditionAsync<TResult>(Expression<Func<T, bool>> predicate, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted") where TResult : class
        {
            using (var connection = _queryDbContext.CreateConnection())
            {
                PropertyInfo[] propertyInfos;
                propertyInfos = typeof(TResult).GetProperties(BindingFlags.Public);
                List<string> formattedColums = new();
                foreach (var property in propertyInfos)
                    formattedColums.Add($"[{property}]");
                string entityName = typeof(T).Name;
                PredicateExpressionVisitor<T> predicateExpressionVisitor = new PredicateExpressionVisitor<T>();
                var condition = predicateExpressionVisitor.ConvertToSql(predicate);
                string query = showDeletedRecords
                    //? $"SELECT {string.Join(",", formattedColums)} FROM [{entityName}] WHERE {predicateExpressionVisitor.Condition}"
                    //: $"SELECT {string.Join(",", formattedColums)} FROM [{entityName}] WHERE {predicateExpressionVisitor.Condition} AND {isDeletedFieldName} = 0";
                    ? $"SELECT * FROM [{entityName}] WHERE {condition}"
                    : $"SELECT * FROM [{entityName}] WHERE {condition} AND {isDeletedFieldName} = 0";
                connection.Open();
                var result = await connection.QueryAsync<TResult>(query);
                connection.Close();
                return result;
            }
        }

        //
        // GetAllAsync
        // 
        public async Task<IEnumerable<T>> GetAllAsync(bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted")
        {
            return await GetAllAsync(showDeletedRecords, isDeletedFieldName, CancellationToken.None);
        }
        public async Task<IEnumerable<T>> GetAllAsync(bool showDeletedRecords, string isDeletedFieldName, CancellationToken cancellationToken)
        {
            string entityName = typeof(T).Name;
            string query = showDeletedRecords
                ? $"SELECT * FROM [{entityName}]"
                : $"SELECT * FROM [{entityName}] where {isDeletedFieldName} = 0";
            using (var connection = _queryDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<T>(query);
                connection.Close();
                return result;
            }
        }
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(string entityName, bool showDeletedRecords = false, string isDeletedFieldName = "IsDeleted") where TResult : class
        {
            return await GetAllAsync<TResult>(entityName, showDeletedRecords, isDeletedFieldName, CancellationToken.None);
        }
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(string entityName, bool showDeletedRecordse, string isDeletedFieldName, CancellationToken cancellationToken) where TResult : class
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = typeof(TResult).GetProperties(BindingFlags.Public);
            List<string> formattedColums = new();
            foreach (var property in propertyInfos)
                formattedColums.Add($"[{property}]");
            string query = showDeletedRecordse
                ? $"SELECT {string.Join(",", formattedColums)} FROM [{entityName}]"
                : $"SELECT {string.Join(",", formattedColums)} FROM [{entityName}] where {isDeletedFieldName} = 0";
            using (var connection = _queryDbContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<TResult>(query);
                connection.Close();
                return result;
            }
        }
    }
}
