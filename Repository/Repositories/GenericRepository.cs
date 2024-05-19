using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Repository.IRepositories;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        #region Attributes

        private DbSet<TEntity> _dbSet;

        #endregion

        #region Constructor

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public async Task<TEntity> AddItemAsync(TEntity model)
        {
            await _dbSet.AddAsync(model).ConfigureAwait(false);
            return model;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<TEntity>> GetFilteredDataAsync(Expression<Func<TEntity, bool>> filterationExpression)
        {
            return await _dbSet
                .Where(filterationExpression)
                .ToListAsync().ConfigureAwait(true);
        }

        public async Task<TEntity> GetItemAsync(Guid id)
        {
            return await _dbSet.FindAsync(id).ConfigureAwait(true);
        }

        public void RemoveItem(TEntity model)
        {
            _dbSet.Remove(model);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void UpdateItem(TEntity model)
        {
            _dbSet.Update(model);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public IQueryable<TEntity> GetDataAsQuery(ref int totalCount, int pageNumber = 0, int pageSize = 10)
        {
            totalCount = _dbSet.Count();
            return _dbSet.Skip((pageNumber * pageSize)).Take(pageSize);
        }

        public IQueryable<TEntity> GetDataAsQuery(Expression<Func<TEntity, bool>> filterationExpression)
        {
            return _dbSet.Where(filterationExpression);
        }

        public IQueryable<TEntity> GetDataAsQuery()
        {
            return _dbSet;
        }

        public IEnumerable<TEntity> GetFilteredData(Expression<Func<TEntity, bool>> filterationExpression)
        {
            return _dbSet
                .Where(filterationExpression)
                .ToList();
        }

        #endregion
    }
}
