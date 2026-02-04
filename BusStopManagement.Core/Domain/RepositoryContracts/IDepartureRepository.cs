using BusStopManagement.Core.Domain.Entities;

namespace BusStopManagement.Core.Domain.RepositoryContracts
{
    public interface IDepartureRepository
    {
        Task<Departure> AddDeparture(Departure departure);

        Task<List<Departure>> GetDepartures();

        Task<bool> DeleteDeparture(Departure departure);

        Task<Departure> UpdateDeparture(Departure departure);
    }
}
