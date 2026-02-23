using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class BusStopDeleterService : IBusStopDeleterService
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopDeleterService(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        public async Task<bool> DeleteBusStop(BusStop busStop)
        {
            if (busStop == null)
                throw new ArgumentNullException(nameof(busStop));
            else
                return await _busStopRepository.DeleteBusStop(busStop);
        }
    }
}
