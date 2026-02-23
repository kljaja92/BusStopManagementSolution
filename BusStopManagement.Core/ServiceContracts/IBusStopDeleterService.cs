using BusStopManagement.Core.Domain.Entities;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IBusStopDeleterService
    {
        Task<bool> DeleteBusStop(BusStop busStop);
    }
}
