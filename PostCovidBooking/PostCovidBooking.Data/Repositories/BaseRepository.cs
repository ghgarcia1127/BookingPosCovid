using Microsoft.EntityFrameworkCore;
using PostCovidBooking.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostCovidBooking.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        #region Fields
        private readonly IQueryableUnitOfWork unitOfWork;
        #endregion

        #region Constructor
        public BaseRepository(IQueryableUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.unitOfWork.GetContext().Database.EnsureCreated();
        }
        #endregion

        #region IBaseRepository Members
        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await unitOfWork.GetSet<TEntity>().AddAsync(entity).ConfigureAwait(false);
            }
            finally
            {
                await unitOfWork.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                unitOfWork.GetSet<TEntity>().Remove(entity);
            }
            finally
            {
                await unitOfWork.CommitAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await BuildQuery(filter, orderBy, includeProperties).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                var dbSet = unitOfWork.GetSet<TEntity>();
                dbSet.Update(entity);
            }
            finally
            {
                await unitOfWork.CommitAsync().ConfigureAwait(false);
            }
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
        }
        #endregion

        #region Private members
        private IQueryable<TEntity> BuildQuery(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = unitOfWork.GetSet<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(property =>
            {
                query = query.Include(property);
            });

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
        #endregion
    }
}
