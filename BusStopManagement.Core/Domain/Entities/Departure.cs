using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusStopManagement.Core.Domain.Entities
{
    public class Departure
    {
        [Key]
        public Guid DepartureID { get; set; }
        
        public string Destination { get; set; } = string.Empty;
        
        public DateTime DateAndTimeOfDeparture { get; set; }
        
        public byte NumberOfSeats { get; set; }

        public Guid BusStopID { get; set; }

        [ForeignKey("BusStopID")]
        public BusStop BusStop { get; set; } = null!;
    }
}
