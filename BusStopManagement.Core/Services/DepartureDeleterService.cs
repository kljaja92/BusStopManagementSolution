using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class DepartureDeleterService : IDepartureDeleterService
    {
        private readonly IDepartureRepository _departureRepository;

        public DepartureDeleterService(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<bool> DeleteDeparture(Departure departure)
        {
            if (departure == null)
                throw new ArgumentNullException(nameof(departure));

            return await _departureRepository.DeleteDeparture(departure);
        }
    }
}
