using Microsoft.AspNetCore.Mvc;
using NathanielJacksonAssignment2.Services;
using NathanielJacksonAssignment2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace NathanielJacksonAssignment2.Controllers
{
    /// <summary>
    /// This is the user controller
    /// The user controller is responsible for methods that display
    /// lists of users from the database
    /// </summary>
    public class UserController : Controller
    {
        private IFlightRepository _flightRepo;
        private readonly IUserRepository _userRepo;

        /// <summary>
        /// Constructor for the user controller
        /// used to inject the user repository and flight repository into the class
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        public UserController(IUserRepository userRepo, IFlightRepository flightRepo)
        {
            _userRepo = userRepo;
            _flightRepo = flightRepo;
        }
        /// <summary>
        /// This method is called if the types /Index into the address bar
        /// Never called otherwise
        /// </summary>
        /// <returns>The view for index</returns>
        public IActionResult Index()
        {
            ICollection<User> model = _userRepo.ReadAll();
            return View(model);
        }

        /// <summary>
        /// This method is called when the user presses "Add passengers" for a specific flight
        /// </summary>
        /// <param name="flightId">What flight are we adding passengers to?</param>
        /// <returns>A view that is given a list of users not currently on the flight</returns>
        public IActionResult Create([Bind(Prefix ="id")]int flightId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                Console.WriteLine("The flight was null returning....");
                return RedirectToAction("Index", "Flight");
            }
            var allUsers = _userRepo.ReadAll();
            var usersOnFlights = flight.ListOfPassengers
                .Select(lop => lop.User).ToList();
            var usersNotOnFlights = allUsers.Except(usersOnFlights);
            ViewData["Flight"] = flight;
            return View(usersNotOnFlights);
        }
    }
}
