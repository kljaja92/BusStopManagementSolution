using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class DepartureGetterService : IDepartureGetterService
    {
        private readonly IDepartureRepository _departureRepository;

        public DepartureGetterService(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<List<DepartureResponse>> GetDepartures()
        {
            List<Departure> departures = await _departureRepository.GetDepartures();

            return departures.Select(x => x.ToDepartureResponse()).ToList();
        }
    }
}
