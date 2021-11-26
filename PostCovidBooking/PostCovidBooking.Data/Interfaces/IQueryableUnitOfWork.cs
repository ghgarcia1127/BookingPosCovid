using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace PostCovidBooking.Data.Interfaces
{
    public interface IQueryableUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        DbContext GetContext();
        DbSet<TEntity> GetSet<TEntity>() where TEntity : class;
    }
}
