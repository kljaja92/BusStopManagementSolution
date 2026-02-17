using BusStopManagement.Core.Domain.Entities;
using BusStopManagement.Core.DTO;

namespace BusStopManagement.Core.Extensions
{
    public static class BusStopExtension
    {
        public static BusStopResponse ToBusStopResponse(this BusStop busStop)
        {
            return new BusStopResponse()
            {
                BusStopID = busStop.BusStopID,
                BusStopName = busStop.BusStopName,
                BusStopAddress = busStop.BusStopAddress
            };
        }
    }
}
