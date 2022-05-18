using Microsoft.AspNetCore.Mvc;
using NathanielJacksonAssignment2.Models.Entities;
using NathanielJacksonAssignment2.Services;

namespace NathanielJacksonAssignment2.Controllers
{
    //sources: d2l lecture notes, other references are in the Views

    /// <summary>
    /// The Flight controller class. Resposible for all incoming Http requests for Flight.
    /// </summary>
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepo;

        /// <summary>
        /// The constructor for the flight controller class. Responsible for injecting the
        /// flight repository into the flight controller object at runtime
        /// </summary>
        /// <param name="flightRepo" purpose="injected the flight repository into the flight controller"></param>
        public FlightController(IFlightRepository flightRepo)
        {
            _flightRepo = flightRepo;
        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Index
        /// Reads all entries from the database and sends them to the view
        /// </summary>
        /// <returns>The Index view for Flight</returns>
        public IActionResult Index()
        {
            ICollection<Flight> model = _flightRepo.ReadAll();
            return View(model);
        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Create
        /// Returns the view for Create
        /// </summary>
        /// <returns>The Create view for Flight</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Called when the system makes an HttpPost request for Flight/Create
        /// Called when the user submitts information from the Create view
        /// If all fields all present, adds a new entry to database
        /// Returns to Flight/Index view if successful
        /// Returns to Flight/Create view if unsuccessful
        /// </summary>
        /// <param name="newFlight" purpose="captures the Flight object sent by the HttpPost request. Contains information entered into the form"></param>
        /// <returns>Either the Flight/Index view or the Flight/Create view</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Flight newFlight)
        {
            if (ModelState.IsValid)
            {
                _flightRepo.Create(newFlight);
                return RedirectToAction("Index");
            }
            return View(newFlight);
        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Details
        /// Returns the view for Details if the designated entry was found in the database
        /// if the entry was not found it returns the view for Flight/Index
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        /// <returns>Either the Flight/Details view or the Flight/Index view</returns>
        public IActionResult Details(int id)
        {
            Flight? FlightVar = _flightRepo.Read(id);
            if (FlightVar == null)
            {
                return RedirectToAction("Index");
            }
            return View(FlightVar);

        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Update
        /// Returns the view for Update if the designated entry was found in the database
        /// if the entry was not found it returns the view for Flight/Index
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        /// <returns>Either the Flight/Update view or the Flight/Index view</returns>
        public IActionResult Update(int id)
        {
            Flight? FlightVar = _flightRepo.Read(id);
            if (FlightVar == null)
            {
                return RedirectToAction("Index");
            }
            return View(FlightVar);
        }

        /// <summary>
        /// Called when the system makes an HttpPost request for Flight/Update
        /// Called when the user submitts information from the Update view
        /// If all fields all present, updates the designated entry in the database
        /// Returns to Flight/Index view if successful
        /// Returns to Flight/Update view if unsuccessful
        /// </summary>
        /// <param name="flight" purpose="captures the Flight object sent by the HttpPost request. Contains information entered into the form"></param>
        /// <returns>Either the Flight/Index view or the Flight/Update view</returns>
        [HttpPost]
        public IActionResult Update(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _flightRepo.Update(flight.Id, flight);
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Delete
        /// Returns the view for Delete if the designated entry was found in the database
        /// if the entry was not found it returns the view for Flight/Index
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        /// <returns>Either the Flight/Delete view or the Flight/Index view</returns>
        public IActionResult Delete(int id)
        {
            Flight? FlightVar = _flightRepo.Read(id);
            if (FlightVar == null)
            {
                return RedirectToAction("Index");
            }
            return View(FlightVar);
        }

        /// <summary>
        /// Called when the system makes an HttpPost request for Flight/Delete
        /// Deletes the selected entry in the database
        /// Selected entry is provided by the form in the view for Flight/Delete
        /// Returns to Flight/Index view
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        /// <returns>The Flight view for Index</returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _flightRepo.Delete(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Called when the system makes an HttpGet request for Flight/Review
        /// Returns the view for Review if the designated entry was found in the database
        /// if the entry was not found it returns the view for Flight/Index
        /// The Review view displays a page where a user can enter a review for the selected flight
        /// </summary>
        /// <param name="id" purpose="Designated entry in the database"></param>
        /// <returns>Either the Flight/Review view or the Flight/Index view</returns>
        public IActionResult Review(int id)
        {
            Flight? FLightVar = _flightRepo.Read(id);
            if (FLightVar == null)
            {
                return RedirectToAction("Index");
            }
            return View(FLightVar);
        }

        /// <summary>
        /// Called when the system makes an HttpPost request for Flight/Review
        /// Called when the user submitts information from the Review view
        /// If all fields all present, updates the review property for the designated entry in the database
        /// Returns to Flight/Index view if successful
        /// Returns to Flight/Review view if unsuccessful
        /// </summary>
        /// <param name="flight" purpose="captures the Flight object sent by the HttpPost request. Contains information entered into the form"></param>
        /// <returns>Either the Flight/Index view or the Flight/Review view</returns>
        /*[HttpPost]
        public IActionResult Review(Flight flight)
        {
            if (ModelState.IsValid)
            {
                _flightRepo.SetFlightReview(flight.Id, flight);
                return RedirectToAction("Index");
            }
            return View(flight);
        }*/
    }
}
