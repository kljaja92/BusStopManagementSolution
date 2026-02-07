using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace BusStopManagement.Infrastructure.Repositories
{
    public class DepartureRepository : IDepartureRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartureRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Departure> AddDeparture(Departure departure)
        {
            _dbContext.Departures.Add(departure);
            await _dbContext.SaveChangesAsync();
            return departure;
        }

        public async Task<bool> DeleteDeparture(Departure departure)
        {
            _dbContext.Departures.Remove(departure);
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Departure>> GetDepartures()
        {
            //return await _dbContext.Departures.Include(x => x.BusStop).ToListAsync();
            return await _dbContext.Departures.ToListAsync();
        }

        public async Task<Departure> UpdateDeparture(Departure departure)
        {
            _dbContext.Departures.Update(departure);
            await _dbContext.SaveChangesAsync();
            return departure;
        }
    }
}
