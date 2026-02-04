using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.Infrastructure.Repositories
{
    public class BusStopRepository : IBusStopRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BusStopRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BusStop> AddBusStop(BusStop busStop)
        {
            _dbContext.BusStops.Add(busStop);
            await _dbContext.SaveChangesAsync();
            return busStop;
        }

        public async Task<bool> DeleteBusStop(BusStop busStop)
        {
            _dbContext.BusStops.Remove(busStop);
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<IEnumerable<BusStop>> GetBusStops()
        {
            return await _dbContext.BusStops.ToListAsync();
        }

        public async Task<BusStop> UpdateBusStop(BusStop busStop)
        {
            _dbContext.BusStops.Update(busStop);
            await _dbContext.SaveChangesAsync();
            return busStop;
        }
    }
}
