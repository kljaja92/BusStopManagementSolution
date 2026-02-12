using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.Domain.Exceptions;
using BusStopManagement.Core.Domain.RepositoryContracts;
using BusStopManagement.Core.DTO;
using BusStopManagement.Core.Extensions;
using BusStopManagement.Core.Helpers;
using BusStopManagement.Core.ServiceContracts;

namespace BusStopManagement.Core.Services
{
    public class DepartureUpdaterService : IDepartureUpdaterService
    {
        private readonly IDepartureRepository _departureRepository;

        public DepartureUpdaterService(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }

        public async Task<DepartureResponse> UpdateDeparture(DepartureUpdateRequest? departureUpdateRequest)
        {
            if (departureUpdateRequest == null)
                throw new ArgumentNullException(nameof(departureUpdateRequest));
            else
            {
                ValidationHelper.ModelValidation(departureUpdateRequest);

                Departure? matchingDeparture = await _departureRepository.GetDepartureByDepartureId(departureUpdateRequest.DepartureID);

                if (matchingDeparture == null)
                    throw new InvalidDepartureIDException("Departure doesn't exist.");
                else
                {
                    matchingDeparture.Destination = departureUpdateRequest.Destination;
                    matchingDeparture.DateAndTimeOfDeparture = departureUpdateRequest.DateAndTimeOfDeparture;
                    matchingDeparture.NumberOfSeats = departureUpdateRequest.NumberOfSeats;
                    matchingDeparture.BusStopID = departureUpdateRequest.BusStopID;

                    await _departureRepository.UpdateDeparture(matchingDeparture);

                    return matchingDeparture.ToDepartureResponse();
                }
            }
        }
    }
}
