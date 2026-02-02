using System.ComponentModel.DataAnnotations;

namespace BusStopManagement.Core.DTO
{
    public class BusStopAddRequest
    {
        [Required(ErrorMessage = "Please enter bus stop name.")]
        [StringLength(100, ErrorMessage = "Bus stop name cannot exceed 100 characters.")]
        public string BusStopName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter bus stop address.")]
        [StringLength(100, ErrorMessage = "Bus stop name cannot exceed 100 characters.")]
        public string BusStopAddress { get; set; } = string.Empty;
    }
}
