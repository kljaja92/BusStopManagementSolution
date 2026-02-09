using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.Extensions
{
    public static class DepartureExtensions
    {
        public static DepartureResponse ToDepartureResponse(this Departure departure)
        {
            return new DepartureResponse()
            {
                DepartureID = departure.DepartureID,
                Destination = departure.Destination,
                DateAndTimeOfDeparture = departure.DateAndTimeOfDeparture,
                NumberOfSeats = departure.NumberOfSeats,
                BusStopID = departure.BusStopID
            };
        }
    }
}
