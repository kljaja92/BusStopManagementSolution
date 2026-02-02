namespace BusStopManagement.Core.Domain.Entities
{
    public class BusStop
    {
        public Guid BusStopID { get; set; }

        public string BusStopName { get; set; } = string.Empty;

        public string BusStopAddress { get; set; } = string.Empty;

        public ICollection<Departure> Departures { get; set; } = new List<Departure>();
    }
}
