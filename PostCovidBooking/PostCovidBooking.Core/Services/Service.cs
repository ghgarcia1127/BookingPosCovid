using AutoMapper;
using PostCovidBooking.Core.Interfaces;
using PostCovidBooking.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PostCovidBooking.Core.Services
{
    public class Service<TEntity, TEntityDto> : IService<TEntity, TEntityDto>
        where TEntity : class
        where TEntityDto : class
    {

        private readonly IBaseRepository<TEntity> repository;
        private readonly IMapper mapper;

        public Service(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task AddAsync(TEntityDto entityDto)
        {
            await repository.AddAsync(mapper.Map<TEntity>(entityDto)).ConfigureAwait(false);
        }

        public async Task DeleteAsync(TEntityDto entity)
        {
            await repository.DeleteAsync(mapper.Map<TEntity>(entity)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntityDto>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return mapper.Map<IEnumerable<TEntityDto>>(await repository.GetAllAsync(filter, orderBy, includeProperties).ConfigureAwait(false));
        }

        public async Task UpdateAsync(TEntityDto entity)
        {
            await repository.UpdateAsync(mapper.Map<TEntity>(entity)).ConfigureAwait(false);
        }
    }
}
