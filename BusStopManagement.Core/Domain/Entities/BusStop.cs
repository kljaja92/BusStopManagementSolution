using System.ComponentModel.DataAnnotations;

namespace BusStopManagement.Core.Domain.Entities
{
    public class BusStop
    {
        [Key]
        public Guid BusStopID { get; set; }

        public ICollection<Departure>? Departures { get; set; } = [];
    }
}
