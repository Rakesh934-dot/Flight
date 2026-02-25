using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flight.Models
{
    public class Flight : IValidatableObject
    {
        public int FlightId { get; set; }

        [Display(Name = "Flight Number")]
        [Required(ErrorMessage = "Flight number is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Flight number must be between 3 and 10 characters.")]
        public string FlightNumber { get; set; } = string.Empty;

        [Display(Name = "From City")]
        [Required(ErrorMessage = "From city is required.")]
        public string FromCityId { get; set; } = string.Empty;

        [Display(Name = "To City")]
        [Required(ErrorMessage = "To city is required.")]
        public string ToCityId { get; set; } = string.Empty;

        [Display(Name = "Departure Date")]
        [Required(ErrorMessage = "Departure date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [FlightDateNotInPast(ErrorMessage = "Departure date cannot be in the past.")]
        public DateTime? Date { get; set; }

        [Display(Name = "Ticket Price")]
        [Required(ErrorMessage = "Ticket price is required.")]
        [Range(typeof(decimal), "0.01", "999999.99", ErrorMessage = "Ticket price must be greater than 0.")]
        public decimal? Price { get; set; }

        public City? FromCity { get; set; }
        public City? ToCity { get; set; }

        public string Slug =>
            (FlightNumber + "-" + FromCity?.Name + "-" + ToCity?.Name)
            .Replace(" ", "-")
            .ToLower();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(FromCityId)
                && !string.IsNullOrWhiteSpace(ToCityId)
                && string.Equals(FromCityId, ToCityId, StringComparison.OrdinalIgnoreCase))
            {
                yield return new ValidationResult(
                    "From city and to city must be different.",
                    new[] { nameof(FromCityId), nameof(ToCityId) });
            }
        }
    }

    public sealed class FlightDateNotInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime date)
            {
                return ValidationResult.Success;
            }

            if (date.Date < DateTime.UtcNow.Date)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
