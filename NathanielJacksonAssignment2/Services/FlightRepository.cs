using NathanielJacksonAssignment2.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace NathanielJacksonAssignment2.Services
{
    /// <summary>
    /// The flight repository class. Works as a link between other classes and the database
    /// </summary>
    public class FlightRepository : IFlightRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// Constructor for the flight repository.
        /// Responsible for injecting the dbContext into the flight repository
        /// </summary>
        /// <param name="db" purpose="the dbContext to be injected"></param>
        public FlightRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This method adds a new entry to database by calling the dbContext
        /// </summary>
        /// <param name="newFlight" purpose="the new flight object that is being added to the database"></param>
        /// <returns>Returns a copy of the flight object that was added to the database</returns>
        public Flight Create(Flight newFlight)
        {
            _db.FlightSchedules.Add(newFlight);
            _db.SaveChanges();
            return newFlight;
        }

        /// <summary>
        /// Deletes a selected entry from the database
        /// First the entry is read from the database
        /// if the entry is found then it is deleted
        /// if it is not found then nothing is done
        /// </summary>
        /// <param name="id" purpose="the id of the selected database entry"></param>
        public void Delete(int id)
        {
            Flight? flightToDelete = Read(id);
            if (flightToDelete != null)
            {
                _db.FlightSchedules.Remove(flightToDelete);
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Reads a selected entry from the database
        /// If no entry is found then null is returned
        /// </summary>
        /// <param name="id" purpose="id of the selected database entry"></param>
        /// <returns>If the database was found then it is returned; otherwise null is returned</returns>
        public Flight? Read(int id)
        {
            Console.WriteLine($"Input id is: {id}");
            return _db.FlightSchedules
                .Include(l => l.ListOfPassengers)
                    .ThenInclude(lop => lop.User)
                .FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Reads every entry from the database
        /// </summary>
        /// <returns>returns a list containing every entry from the database</returns>
        public ICollection<Flight> ReadAll()
        {
            return _db.FlightSchedules
                .Include(fs => fs.ListOfPassengers)
                    .ThenInclude(lop => lop.User)
                .ToList();
        }

        /// <summary>
        /// Sets the review property for the selected database entry
        /// if the selected entry is not found then nothing is done
        /// otherwise the selected entries review property is set to the new review property
        /// the new review property is stored in an incoming Flight object
        /// </summary>
        /// <param name="oldId" purpose="Id of the selected database entry"></param>
        /// <param name="flight" purpose="stores the new properties"></param>
        /*public void SetFlightReview(int oldId, Flight flight)
        {
            Flight? flightToReview = Read(oldId);
            if (flightToReview != null)
            {
                flightToReview.Review = flight.Review;
                _db.SaveChanges();
            }
        }*/

        /// <summary>
        /// Sets all properties for the selected database entry
        /// Review is omitted
        /// if the selected entry is not found then nothing is done
        /// otherwise the selected entry has all of its properties set to the new incoming properties
        /// the new properties are stored in an incoming Flight object
        /// </summary>
        /// <param name="oldId" purpose="Id of the selected database entry"></param>
        /// <param name="flight" purpose="stores the new properties"></param>
        public void Update(int oldId, Flight flight)
        {
            Flight? flightToUpdate = Read(oldId);
            if (flightToUpdate != null)
            {
                flightToUpdate.Destination = flight.Destination;
                flightToUpdate.StartLocation = flight.StartLocation;
                flightToUpdate.ArrivalTime = flight.ArrivalTime;
                flightToUpdate.DepartureTime = flight.DepartureTime;
                _db.SaveChanges();
            }
        }
    }
}
