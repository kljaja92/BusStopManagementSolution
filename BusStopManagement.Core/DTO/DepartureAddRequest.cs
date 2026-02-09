using BusStopManagement.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusStopManagement.Core.DTO
{
    public class DepartureAddRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Please enter destination.")]
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter date and time of departure.")]
        public DateTime DateAndTimeOfDeparture { get; set; }

        [Required(ErrorMessage = "Please enter number of seats.")]
        [Range(1, byte.MaxValue, ErrorMessage = "Entered seats must be greater than 0.")]
        public byte NumberOfSeats { get; set; }

        [Required(ErrorMessage = "Please choose a bus stop.")]
        public Guid BusStopID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DateAndTimeOfDeparture <= DateTime.UtcNow)
                yield return new ValidationResult("Departure time must be in the future.", new[] { nameof(DateAndTimeOfDeparture) });
        }

        public Departure ToDeparture()
        {
            return new Departure()
            {
                Destination = Destination,
                DateAndTimeOfDeparture = DateAndTimeOfDeparture,
                NumberOfSeats = NumberOfSeats,
                BusStopID = BusStopID
            };
        }
    }
}
