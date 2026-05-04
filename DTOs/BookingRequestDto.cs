using System.ComponentModel.DataAnnotations;

public class BookingRequestDto
{
    [Required(ErrorMessage = "FlightId is required.")]
    [MinLength(1, ErrorMessage = "FlightId cannot be empty.")]
    public string FlightNumber { get; set; } = string.Empty;




    [Required(ErrorMessage = "Origin is required.")]
    [MinLength(1, ErrorMessage = "Origin cannot be empty.")]
    public string Origin { get; set; } = string.Empty;

    [Required(ErrorMessage = "Destination is required.")]
    [MinLength(1, ErrorMessage = "Destination cannot be empty.")]
    public string Destination { get; set; } = string.Empty;

    

    [Range(1, 9, ErrorMessage = "Passengers must be between 1 and 9.")]
    public int Passengers { get; set; }


    [Required(ErrorMessage = "FullName is required.")]
    [MinLength(1, ErrorMessage = "FullName cannot be empty.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "EmailAddress is required.")]
    [MinLength(1, ErrorMessage = "EmailAddress cannot be empty.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "DocumentNumber is required.")]
    [MinLength(1, ErrorMessage = "DocumentNumber cannot be empty.")]
    public string DocumentNumber { get; set; } = string.Empty;
}