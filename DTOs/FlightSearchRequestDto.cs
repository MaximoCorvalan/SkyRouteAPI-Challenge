using System.ComponentModel.DataAnnotations;

namespace SkyRouteAPI.DTOs
{
    public class FlightSearchRequestDto
    {
        [Required(ErrorMessage = "Origin is required.")]
        [MinLength(1, ErrorMessage = "Origin cannot be empty.")]
        [RegularExpression(@"\S+", ErrorMessage = "Origin cannot be only spaces.")]
        public string Origin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Destination is required.")]
        [MinLength(1, ErrorMessage = "Destination cannot be empty.")]
        [RegularExpression(@"\S+", ErrorMessage = "Destination cannot be only spaces.")]
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "DepartureDate is required.")]
        public DateTime DepartureDate { get; set; }

        [Range(1, 9, ErrorMessage = "Passengers must be between 1 and 9.")]
        public int Passengers { get; set; }

        [Required(ErrorMessage = "CabinClass is required.")]
        [MinLength(1, ErrorMessage = "CabinClass cannot be empty.")]
        [RegularExpression(@"\S+", ErrorMessage = "CabinClass cannot be only spaces.")]
        public string CabinClass { get; set; } = "Economy";
    }
}
