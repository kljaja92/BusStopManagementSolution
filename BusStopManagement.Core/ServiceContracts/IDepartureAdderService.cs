using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.ServiceContracts
{
    public interface IDepartureAdderService
    {
        Task<DepartureResponse> AddDeparture(DepartureAddRequest? departureAddRequest);
    }
}
