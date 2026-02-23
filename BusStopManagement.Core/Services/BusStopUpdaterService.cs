using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.Exceptions;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.Helpers;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class BusStopUpdaterService : IBusStopUpdaterService
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopUpdaterService(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        public async Task<BusStopResponse> UpdateBusStop(BusStopUpdateRequest? busStopUpdateRequest)
        {
            if (busStopUpdateRequest == null)
                throw new ArgumentNullException(nameof(busStopUpdateRequest));
            else
            {
                ValidationHelper.ModelValidation(busStopUpdateRequest);

                BusStop? matchingBusStop = await _busStopRepository.GetBusStopByBusStopId(busStopUpdateRequest.BusStopID);

                if (matchingBusStop == null)
                    throw new InvalidBusStopIDException("Bus stop doesn't exist.");
                else
                {
                    matchingBusStop.BusStopName = busStopUpdateRequest.BusStopName;
                    matchingBusStop.BusStopAddress = busStopUpdateRequest.BusStopAddress;

                    await _busStopRepository.UpdateBusStop(matchingBusStop);

                    return matchingBusStop.ToBusStopResponse();
                }
            }
        }
    }
}
