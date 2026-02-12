using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IDepartureUpdaterService
    {
        Task<DepartureResponse> UpdateDeparture(DepartureUpdateRequest? departureUpdateRequest);
    }
}
