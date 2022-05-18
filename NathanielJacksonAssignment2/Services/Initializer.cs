using NathanielJacksonAssignment2.Models.Entities;

namespace NathanielJacksonAssignment2.Services
{
    /// <summary>
    /// this class seeds the database
    /// </summary>
    public class Initializer
    {
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// this is the constructor for the Initializer
        /// this is responsible for injecting the db context into the class
        /// </summary>
        /// <param name="db"></param>
        public Initializer(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This method seeds the database with new entries if no entries were found
        /// </summary>
        public void SeedDatabase()
        {
            Console.WriteLine("Seed database Called");
            _db.Database.EnsureCreated();

            if (_db.Users.Any())
            {
                Console.WriteLine("Users already exist. Not seeding.");
                return;
            }

            var users = new List<User>()
            {
                new User { UserName = "SamWilson", Password = "Space"},
                new User { UserName = "JohnnyCash", Password = "Yeet"},
                new User { UserName = "BobNewhart", Password = "Yoink"}
            };

            _db.Users.AddRange(users);
            _db.SaveChanges();


        }
    }
}
