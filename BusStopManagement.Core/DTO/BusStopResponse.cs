using BusStopManagement.Core.Domain.Entities;

namespace BusStopManagement.Core.DTO
{
    public class BusStopResponse
    {
        public Guid BusStopID { get; set; }

        public string BusStopName { get; set; } = string.Empty;

        public string BusStopAddress { get; set; } = string.Empty;

        public ICollection<DepartureResponse> Departures { get; set; } = [];

        public override bool Equals(object? obj) =>
            obj is BusStopResponse other &&
            BusStopID == other.BusStopID &&
            BusStopName == other.BusStopName &&
            BusStopAddress == other.BusStopAddress;

        public override int GetHashCode() =>
            HashCode.Combine(BusStopID, BusStopName, BusStopAddress);

        public override string ToString() =>
            $"Bus stop ID: {BusStopID}, Bus stop name: {BusStopName}, Bus stop address: {BusStopAddress}";
    }
}
