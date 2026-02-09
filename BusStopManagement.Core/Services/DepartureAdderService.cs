using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.Helpers;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class DepartureAdderService : IDepartureAdderService
    {
        private readonly IDepartureRepository _departureRepository;

        public DepartureAdderService(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<DepartureResponse> AddDeparture(DepartureAddRequest? departureAddRequest)
        {
            if (departureAddRequest == null)
                throw new ArgumentNullException(nameof(departureAddRequest));

            ValidationHelper.ModelValidation(departureAddRequest);

            Departure departure = departureAddRequest.ToDeparture();

            departure.DepartureID = Guid.NewGuid();

            await _departureRepository.AddDeparture(departure);

            return departure.ToDepartureResponse();
        }
    }
}
