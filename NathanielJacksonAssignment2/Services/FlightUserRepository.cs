using Microsoft.EntityFrameworkCore;
using NathanielJacksonAssignment2.Models.Entities;
using NathanielJacksonAssignment2.Models;

namespace NathanielJacksonAssignment2.Services
{
    /// <summary>
    /// this repository is responsible for all CRUD operations
    /// related to registering users for flights
    /// </summary>
    public class FlightUserRepository : IFlightUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IFlightRepository _flightRepo;
        private readonly IUserRepository _userRepo;

        /// <summary>
        /// the constructor for the flightUser repository
        /// this method is responsible for injecting the db context,
        /// flight repository, and user repository into the class.
        /// </summary>
        /// <param name="db">the db context</param>
        /// <param name="flightRepo">the flight repository</param>
        /// <param name="userRepo">the user repository</param>
        public FlightUserRepository (ApplicationDbContext db, IFlightRepository flightRepo, IUserRepository userRepo)
        {
            _db = db;
            _flightRepo = flightRepo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Reads a specific entry from the FlightUsers table in the database
        /// </summary>
        /// <param name="id">the specific entry to be read</param>
        /// <returns>The FlightUser object if it was found</returns>
        public FlightUser? Read(int id)
        {
            return _db.FlightUsers
                .Include(fu => fu.Flight)
                .Include(fu => fu.User)
                .FirstOrDefault(fu => fu.Id == id);
        }

        /// <summary>
        /// Reads all entries from the FlightUsers table in the database
        /// </summary>
        /// <returns>A list containing all FlightUsers from the database</returns>
        public ICollection<FlightUser> ReadAll()
        {
            return _db.FlightUsers
                .Include(fu => fu.Flight)
                .Include(fu => fu.User)
                .ToList();
        }

        /// <summary>
        /// This method creates a new FlightUser
        /// this represents a User registration for a specific flight
        /// </summary>
        /// <param name="flightId">The flight being registered for</param>
        /// <param name="userId">The user registering for the flight</param>
        /// <returns>The new FlightUser object representing the registration</returns>
        public FlightUser? Create(int flightId, int userId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                return null;
            }
            var flightUser = flight.ListOfPassengers
                .FirstOrDefault(fu => fu.UserID == userId);
            if (flightUser != null)
            {
                return null;
            }
            var user = _userRepo.Read(userId);
            if (user == null)
            {
                return null;
            }

            var newFlightUser = new FlightUser
            {
                Flight = flight,
                User = user
            };
            flight.ListOfPassengers.Add(newFlightUser);
            user.BookedFlights.Add(newFlightUser);
            _db.SaveChanges();
            return newFlightUser;
        }

        /// <summary>
        /// Updates a user's flight class for a specific flight
        /// </summary>
        /// <param name="flightUserId">The flight that was registered for this user</param>
        /// <param name="flightClass">The new flight class</param>
        public void UpdateFlightClass(int flightUserId, string flightClass)
        {
            var flightUser = Read(flightUserId);
            if (flightUser != null)
            {
                flightUser.FlightClass = flightClass;
                flightUser.CostOfFlightUSD = FlightPrices.PricesPerClass[flightClass];
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// updates a user's review for a specific flight
        /// </summary>
        /// <param name="flightUserId">The registration for this specific flight</param>
        /// <param name="Review">The new review text</param>
        public void UpdateFlightReview(int flightUserId, string Review)
        {
            var flightUser = Read(flightUserId);
            if (flightUser != null)
            {
                flightUser.Review = Review;
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// removes a user from a flight by
        /// deleting their flightUser registration class
        /// </summary>
        /// <param name="flightId">The flight that was registered for</param>
        /// <param name="flightUserId">the registration a specific user had for this flight</param>
        public void Remove(int flightId, int flightUserId)
        {
            var flight = _flightRepo.Read(flightId);
            var flightUser = flight!.ListOfPassengers
                .FirstOrDefault(lop => lop.Id == flightUserId);
            var user = flightUser!.User;
            flight!.ListOfPassengers.Remove(flightUser);
            user!.BookedFlights.Remove(flightUser);
            _db.SaveChanges();
        }
    }
}
