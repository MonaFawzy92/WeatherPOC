using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetItemAsync(Guid id);
        Task<TEntity> AddItemAsync(TEntity model);
        void UpdateItem(TEntity model);
        void RemoveItem(TEntity model);
        Task<IEnumerable<TEntity>> GetFilteredDataAsync(Expression<Func<TEntity, bool>> filterationExpression);
        IEnumerable<TEntity> GetFilteredData(Expression<Func<TEntity, bool>> filterationExpression);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void UpdateRange(IEnumerable<TEntity> entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        IQueryable<TEntity> GetDataAsQuery(ref int totalCount, int pageNumber, int pageSize);
        IQueryable<TEntity> GetDataAsQuery();
        IQueryable<TEntity> GetDataAsQuery(Expression<Func<TEntity, bool>> filterationExpression);
    }
}
