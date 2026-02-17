using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IBusStopGetterService
    {
        Task<List<BusStopResponse>> GetBusStops();

        Task<BusStopResponse?> GetBusStopByBusStopID(Guid? busStopID);

        Task<BusStopResponse?> GetBusStopByBusStopName(string busStopName);
    }
}
