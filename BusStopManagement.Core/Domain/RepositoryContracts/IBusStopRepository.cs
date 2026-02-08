using BusStopManagement.Core.Domain.Entities;

namespace BusStopManagement.Core.Domain.RepositoryContracts
{
    public interface IBusStopRepository
    {
        Task<BusStop> AddBusStop(BusStop busStop);

        Task<List<BusStop>> GetBusStops();

        Task<bool> DeleteBusStop(BusStop busStop);

        Task<BusStop> UpdateBusStop(BusStop busStop);
    }
}
