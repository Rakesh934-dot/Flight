using Microsoft.EntityFrameworkCore;

namespace Flight.Models
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Cities
            modelBuilder.Entity<City>().HasData(
                new City { CityId = "CHI", Name = "Chicago" },
                new City { CityId = "NYC", Name = "New York" },
                new City { CityId = "DXB", Name = "Dubai" },
                new City { CityId = "LON", Name = "London" },
                new City { CityId = "HKG", Name = "Hong Kong" },
                new City { CityId = "SFO", Name = "San Francisco" }
            );

            // Seed Flights
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    FlightId = 1,
                    FlightNumber = "UA3321",
                    FromCityId = "CHI",
                    ToCityId = "NYC",
                    Date = new DateTime(2026, 2, 15),
                    Price = 235
                },
                new Flight
                {
                    FlightId = 2,
                    FlightNumber = "QA1078",
                    FromCityId = "DXB",
                    ToCityId = "LON",
                    Date = new DateTime(2026, 3, 1),
                    Price = 590
                },
                new Flight
                {
                    FlightId = 3,
                    FlightNumber = "CA9087",
                    FromCityId = "HKG",
                    ToCityId = "SFO",
                    Date = new DateTime(2026, 6, 15),
                    Price = 900
                }
            );
        }
    }
}
