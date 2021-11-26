using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostCovidBooking.Core.Interfaces
{
    public interface IService<TEntity, TEntityDto>
       where TEntity : class
       where TEntityDto : class
    {
        Task AddAsync(TEntityDto entityDto);
        Task DeleteAsync(TEntityDto entity);
        Task<IEnumerable<TEntityDto>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task UpdateAsync(TEntityDto entity);
    }
}
