using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IBusStopUpdaterService
    {
        Task<BusStopResponse> UpdateBusStop(BusStopUpdateRequest? busStopUpdateRequest);
    }
}
