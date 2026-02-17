using BusStopManagement.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusStopManagement.Core.DTO
{
    public class BusStopUpdateRequest
    {
        [Required(ErrorMessage = "BusStopID can't be blank. ")]
        public Guid BusStopID { get; set; }

        [Required(ErrorMessage = "Please enter bus stop name.")]
        [StringLength(100, ErrorMessage = "Bus stop name cannot exceed 100 characters.")]
        public string BusStopName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter bus stop address.")]
        [StringLength(100, ErrorMessage = "Bus stop name cannot exceed 100 characters.")]
        public string BusStopAddress { get; set; } = string.Empty;

        public BusStop ToBusStop()
        {
            return new BusStop()
            {
                BusStopID = BusStopID,
                BusStopName = BusStopName,
                BusStopAddress = BusStopAddress
            };
        }
    }
}
