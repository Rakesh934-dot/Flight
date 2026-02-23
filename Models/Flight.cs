using System;
using System.ComponentModel.DataAnnotations;

namespace Flight.Models
{
    public class Flight
    {
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Enter flight number.")]
        public string FlightNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Select a city.")]
        public string FromCityId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Select a city.")]
        public string ToCityId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enter a date.")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "Enter price.")]
        public decimal? Price { get; set; }

        public City? FromCity { get; set; }
        public City? ToCity { get; set; }

        public string Slug =>
            (FlightNumber + "-" + FromCity?.Name + "-" + ToCity?.Name)
            .Replace(" ", "-")
            .ToLower();
    }
}
