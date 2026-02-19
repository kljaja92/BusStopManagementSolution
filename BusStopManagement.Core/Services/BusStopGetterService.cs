using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class BusStopGetterService : IBusStopGetterService
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopGetterService(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        public async Task<BusStopResponse?> GetBusStopByBusStopID(Guid? busStopID)
        {
            if (busStopID == null)
                return null;
            else
            {
                BusStop? busStopFromList = await _busStopRepository.GetBusStopByBusStopId(busStopID.Value);

                if (busStopFromList == null)
                    return null;
                else
                    return busStopFromList.ToBusStopResponse();
            }
        }

        public async Task<BusStopResponse?> GetBusStopByBusStopName(string busStopName)
        {
            if (busStopName == null || string.IsNullOrWhiteSpace(busStopName))
                return null;
            else
            {
                BusStop? busStopFromList = await _busStopRepository.GetBusStopByBusStopName(busStopName);

                if (busStopFromList == null)
                    return null;
                else
                    return busStopFromList.ToBusStopResponse();
            }
        }

        public async Task<List<BusStopResponse>> GetBusStops()
        {
            return (await _busStopRepository.GetBusStops()).Select(x => x.ToBusStopResponse()).ToList();
        }
    }
}
