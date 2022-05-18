
using NathanielJacksonAssignment2.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace NathanielJacksonAssignment2.Services
{
    /// <summary>
    /// this class is responsible for CRUD operations related to users
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _db;


        /// <summary>
        /// constructor for the user repository
        /// responsible for injecting the db context into the class
        /// </summary>
        /// <param name="db">the db context</param>
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This method reads a specific user from the database
        /// </summary>
        /// <param name="id">the user to read</param>
        /// <returns>the User object if found</returns>
        public User? Read(int id)
        {
            return _db.Users
                .Include(u => u.BookedFlights)
                    .ThenInclude(bf => bf.Flight) 
                .FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// reads all users from the database
        /// </summary>
        /// <returns>A list containing all users in the database</returns>
        public ICollection<User> ReadAll()
        {
            return _db.Users
                .Include(u => u.BookedFlights)
                    .ThenInclude(bf => bf.Flight)
                .ToList();
        }
    }
}
