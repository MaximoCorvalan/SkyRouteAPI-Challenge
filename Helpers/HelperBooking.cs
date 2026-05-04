namespace SkyRouteAPI.Helpers
{
    /// <summary>
    /// Provides helper methods for booking validation.
    /// </summary>
    public static class HelperBooking
    {
        /// <summary>
        /// Validates the passenger document number according to the route type.
        /// </summary>
        /// <param name="documentNumber">
        /// Passenger document number entered in the booking form.
        /// </param>
        /// <param name="isInternational">
        /// Indicates whether the selected flight is international.
        /// If true, the document is validated as a passport number.
        /// If false, the document is validated as a national ID.
        /// </param>
        /// <returns>
        /// Returns a validation result containing the validation state and a message.
        /// </returns>
        public static (bool state, string message) ValidatePassengerDocument( string documentNumber,  bool isInternational)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
            {
                return isInternational   ? (false, "Passport Number is required."): (false, "National ID is required.");
            }

            if (isInternational)
            {
                var isValidPassport = documentNumber.All(char.IsLetterOrDigit)
                                      && documentNumber.Length >= 6
                                      && documentNumber.Length <= 9;

                if (!isValidPassport)
                    return (false, "Invalid Passport Number.");
            }
            else
            {
                var isValidNationalId = documentNumber.All(char.IsDigit)
                                        && documentNumber.Length >= 7
                                        && documentNumber.Length <= 8;

                if (!isValidNationalId)
                    return (false, "Invalid National ID.");
            }

            return (true, "Document is valid.");
        }
    }
}