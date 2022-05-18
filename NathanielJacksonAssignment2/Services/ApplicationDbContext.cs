using Microsoft.EntityFrameworkCore;
using NathanielJacksonAssignment2.Models.Entities;

namespace NathanielJacksonAssignment2.Services
{
    /// <summary>
    /// This class is a link to the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// this is the constructor for the object.
        /// The base constructor for the DbContext class is called
        /// </summary>
        /// <param name="options" purpose="any options that the system passes to the object"></param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// The internal property used to communicate with the database
        /// </summary>
        public DbSet<Flight> FlightSchedules => Set<Flight>();
        /// <summary>
        /// represents the Users table in the database
        /// </summary>
        public DbSet<User> Users => Set<User>();
        /// <summary>
        /// represents the FlightUsers table in the database
        /// </summary>
        public DbSet<FlightUser> FlightUsers => Set<FlightUser>();
    }
}
