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

        public async Task<DepartureResponse?> GetDepartureByDepartureID(Guid? departureID)
        {
            if (departureID == null)
                return null;
            else
            {
                Departure? departure = await _departureRepository.GetDepartureByDepartureId(departureID.Value);

                if (departure == null)
                    return null;
                else
                    return departure.ToDepartureResponse();
            }
        }

        public async Task<List<DepartureResponse>> GetDepartures()
        {
            List<Departure> departures = await _departureRepository.GetDepartures();

            return departures.Select(x => x.ToDepartureResponse()).ToList();
        }
    }
}
