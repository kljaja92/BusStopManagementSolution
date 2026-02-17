using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.Exceptions;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.Helpers;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class BusStopAdderService : IBusStopAdderService
    {
        private readonly IBusStopRepository _busStopRepository;

        public BusStopAdderService(IBusStopRepository busStopRepository)
        {
            _busStopRepository = busStopRepository;
        }

        public async Task<BusStopResponse> AddBusStop(BusStopAddRequest busStopAddRequest)
        {
            if (busStopAddRequest == null)
                throw new ArgumentNullException(nameof(busStopAddRequest));

            ValidationHelper.ModelValidation(busStopAddRequest);

            if (await _busStopRepository.GetBusStopByBusStopName(busStopAddRequest.BusStopName) != null)
                throw new DuplicateBusStopNameException("Bus stop name already exists.");

            BusStop busStop = busStopAddRequest.ToBusStop();

            busStop.BusStopID = Guid.NewGuid();

            await _busStopRepository.AddBusStop(busStop);

            return busStop.ToBusStopResponse();
        }
    }
}
