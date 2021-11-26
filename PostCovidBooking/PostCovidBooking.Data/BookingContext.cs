using Microsoft.EntityFrameworkCore;
using PostCovidBooking.Data.Interfaces;
using PostCovidBooking.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PostCovidBooking.Data
{
    public class BookingContext : DbContext, IQueryableUnitOfWork
    {
        private readonly string container;

        public virtual DbSet<Reservation> Reservations { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
            this.container = nameof(Reservations);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultContainer(container);

            modelBuilder.Entity<Reservation>()
                .HasNoDiscriminator()
                .ToContainer(container)
                .HasPartitionKey(o => o.Email);

        }
        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await ex.Entries.Single().ReloadAsync().ConfigureAwait(false);
            }
        }

        public DbContext GetContext()
        {
            return this;
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }


    }
}
