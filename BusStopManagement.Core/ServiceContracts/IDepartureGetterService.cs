using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IDepartureGetterService
    {
        Task<List<DepartureResponse>> GetDepartures();
    }
}
