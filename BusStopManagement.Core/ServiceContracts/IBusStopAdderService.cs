using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IBusStopAdderService
    {
        Task<BusStopResponse> AddBusStop(BusStopAddRequest busStopAddRequest);
    }
}
