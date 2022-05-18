using Microsoft.AspNetCore.Mvc;
using NathanielJacksonAssignment2.Models.Entities;
using NathanielJacksonAssignment2.Models.ViewModels;
using NathanielJacksonAssignment2.Services;

namespace NathanielJacksonAssignment2.Controllers
{
    /// <summary>
    /// This controller handles most actions related to 
    /// registering users for flights
    /// </summary>
    public class FlightUserController : Controller
    {
        private readonly IFlightUserRepository _flightUserRepo;
        private IFlightRepository _flightRepo;
        private IUserRepository _userRepo;

        /// <summary>
        /// The constructor for the flight user controller
        /// Resposible for injecting the flight repository,
        /// the user repository, and the flightUser repository
        /// into the class
        /// </summary>
        /// <param name="flightRepo">flight repository</param>
        /// <param name="userRepo">user repository</param>
        /// <param name="flightUserRepo">flightUser repository</param>
        public FlightUserController(IFlightRepository flightRepo, IUserRepository userRepo, IFlightUserRepository flightUserRepo)
        {
            _flightUserRepo = flightUserRepo;
            _flightRepo = flightRepo;
            _userRepo = userRepo;
        }
        /// <summary>
        /// This method is never called unless the user types /FlightUser into the address bar
        /// used for testing only
        /// </summary>
        /// <returns>The index view with a list of all flightUser objects</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The create method is called when the user selects what passenger they want
        /// to add the current flight
        /// </summary>
        /// <param name="flightId">Current flight</param>
        /// <param name="userId">New passenger being added</param>
        /// <returns>the create view with user and flight information</returns>
        public IActionResult Create(int flightId, int userId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                return RedirectToAction("Index", "Flight");
            }

            var user = _userRepo.Read(userId);
            if (user == null)
            {
                return RedirectToAction("Details", "Flight", new { id = flightId });
            }

            var flightUser = flight.ListOfPassengers
                .SingleOrDefault(lop => lop.UserID == userId);
            if (flightUser != null)
            {
                return RedirectToAction("Details", "Flight", new { id = flightId });
            }
            var flightUserVM = new FlightUserVM
            {
                Flight = flight,
                User = user
            };

            return View(flightUserVM);
        }

        /// <summary>
        /// called when the user presses "add passenger" on the confirm screen
        /// </summary>
        /// <param name="flightId">flight that the passenger is being added to</param>
        /// <param name="userId">the new passenger</param>
        /// <returns>the details view for the specified flight</returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public IActionResult CreateConfirmed(int flightId, int userId)
        {
            _flightUserRepo.Create(flightId, userId);
            return RedirectToAction("Details", "Flight", new { id = flightId });
        }

        /// <summary>
        /// called when the user wants to update a passengers flight class
        /// </summary>
        /// <param name="flightId">the flight the passenger is on</param>
        /// <param name="userId">the passenger</param>
        /// <returns>if function is successful returns a view where the user can change the passenger flight class</returns>
        public IActionResult UpdateFlightClass(int flightId, int userId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                return RedirectToAction("Index", "Flight");
            }
            var flightUser = flight.ListOfPassengers
                .FirstOrDefault(lop => lop.UserID == userId);
            if (flightUser == null)
            {
                return RedirectToAction("Details", "Flight", new { id = flightId });
            }
            return View(flightUser);
        }

        /// <summary>
        /// called when the user saves changes to the passengers flight class
        /// </summary>
        /// <param name="flightUserId">the flight registration class for this specific flight and user</param>
        /// <param name="flightId">the flight the passenger is on</param>
        /// <param name="FlightClass">the new flight class that the user has selected</param>
        /// <returns>details for view for flight</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateFlightClass(int flightUserId, int flightId, string FlightClass)
        {
            _flightUserRepo.UpdateFlightClass(flightUserId, FlightClass);
            return RedirectToAction("Details", "Flight", new { Id = flightId });
        }

        /// <summary>
        /// called when the user wants to update a specific passenger's review
        /// for that flight
        /// </summary>
        /// <param name="flightId">The flight that the passenger is on</param>
        /// <param name="userId">the passenger</param>
        /// <returns>if successful returns a view where the user can write a review for that passenger</returns>
        public IActionResult UpdateFlightReview(int flightId, int userId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                return RedirectToAction("Index", "Flight");
            }
            var flightUser = flight.ListOfPassengers
                .FirstOrDefault(lop => lop.UserID == userId);
            if (flightUser == null)
            {
                return RedirectToAction("Details", "Flight", new { id = flightId });
            }
            return View(flightUser);
        }

        /// <summary>
        /// called when the user saves a new review for a specific user
        /// </summary>
        /// <param name="flightUserId">the flight registration class for this user and flight</param>
        /// <param name="flightId">the flight that the passenger is on</param>
        /// <param name="Review">the new review that was written by the user</param>
        /// <returns>the details view for flight</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UpdateFlightReviewSubmit(int flightUserId, int flightId, string Review)
        {
            _flightUserRepo.UpdateFlightReview(flightUserId, Review);
            return RedirectToAction("Details", "Flight", new { Id = flightId });
        }

        /// <summary>
        /// called when the user selects to remove a specific user from the current flight
        /// </summary>
        /// <param name="flightId">the flight that the passenger is on</param>
        /// <param name="userId">the passenger</param>
        /// <returns>a view prompting the user to confirm their delete request</returns>
        public IActionResult Remove(int flightId, int userId)
        {
            var flight = _flightRepo.Read(flightId);
            if (flight == null)
            {
                return RedirectToAction("Index", "Flight");
            }
            var flightUser = flight.ListOfPassengers
                .FirstOrDefault(lop => lop.UserID == userId);
            if (flightUser == null)
            {
                return RedirectToAction("Details", "Flight", new { id = flightId });
            }
            return View(flightUser);
        }

        /// <summary>
        /// called when the user confirms a delete request for
        /// removing a specific passenger from a flight
        /// </summary>
        /// <param name="flightId">the flight that the passenger is on</param>
        /// <param name="flightUserId">the flight registration class for the specific flight and user</param>
        /// <returns>the details view for flight</returns>
        [HttpPost, ValidateAntiForgeryToken, ActionName("Remove")]
        public IActionResult RemoveConfirmed(int flightId, int flightUserId)
        {
            _flightUserRepo.Remove(flightId, flightUserId);
            return RedirectToAction("Details", "Flight", new { id = flightId });
        }

        /// <summary>
        /// Called when the user selects "User Reviews" from the
        /// top screen navigation bar
        /// </summary>
        /// <returns>A view displaying all users in the system</returns>
        public IActionResult ViewUsers()
        {
            ICollection<User> model = _userRepo.ReadAll();
            return View(model);
        }

        /// <summary>
        /// called when the user wants to view all reviews for a specific user
        /// </summary>
        /// <param name="userId">the user that was selected</param>
        /// <returns>A view listing all reviews by that user</returns>
        public IActionResult ViewUserReviews(int userId)
        {
            var user = _userRepo.Read(userId);
            if (user == null)
            {
                return RedirectToAction("ViewUsers");
            }
            ICollection<FlightUser> model = user.BookedFlights.ToList();
            ViewData["User"] = user;
            return View(model);
        }
    }
}
