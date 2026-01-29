using BusStopManagement.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<BusStop> BusStops { get; set; }

        public DbSet<Departure> Departures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BusStop>().ToTable(nameof(BusStop));
            modelBuilder.Entity<Departure>().ToTable(nameof(Departure));

            modelBuilder.Entity<BusStop>().HasMany(x => x.Departures).WithOne(x => x.BusStop).HasForeignKey(x => x.BusStopID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
