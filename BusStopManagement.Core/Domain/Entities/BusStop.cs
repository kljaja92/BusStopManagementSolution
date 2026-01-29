namespace BusStopManagement.Core.Domain.Entities
{
    public class BusStop
    {
        public Guid BusStopID { get; set; }

        public ICollection<Departure>? Departures { get; set; } = [];
    }
}
