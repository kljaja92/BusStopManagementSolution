using BusStopManagement.Core.Domain.Entities;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IDepartureDeleterService
    {
        Task<bool> DeleteDeparture(Departure departure);
    }
}
