using Microsoft.AspNetCore.Mvc;
using NathanielJacksonAssignment2.Services;
using NathanielJacksonAssignment2.Models.Entities;

namespace NathanielJacksonAssignment2.Controllers
{
    /// <summary>
    /// This is the api controller. This controller handles CRUD operations
    /// for FlightUser data models. These methods are called by ajax through fetch.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlightUserApiController : ControllerBase
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IUserRepository _userRepo;
        private readonly IFlightUserRepository _flightUserRepo;

        /// <summary>
        /// This is the controller for the api controller.
        /// This controller injects all of the repositories into the class
        /// </summary>
        /// <param name="flightRepo"></param>
        /// <param name="userRepo"></param>
        /// <param name="flightUserRepo"></param>
        public FlightUserApiController(IFlightRepository flightRepo, IUserRepository userRepo, IFlightUserRepository flightUserRepo)
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _flightUserRepo = flightUserRepo;
        }

        /// <summary>
        /// This method is called when the browser is directed to flightuser/index
        /// A javascriipt file makes the call through fetch
        /// </summary>
        /// <returns>An Ok data model containing information to be displayed on the index page</returns>
        [HttpGet("flightregistrationreport/{id}")]
        public IActionResult Get(int id)
        {
            var flights = _flightRepo.ReadAll();
            var flightUsers =_flightUserRepo.ReadAll();

            var model = from f in flights
                        join fu in flightUsers
                            on f.Id equals fu.FlightID
                        orderby f.Id, fu.User!.UserName

                        select new
                        {
                            FlightId = f.Id,
                            PassengerName = fu.User!.UserName,
                            FlightCost = fu.CostOfFlightUSD,
                            FlightClass = fu.FlightClass
                        };

            Console.WriteLine($"The input was {id}");
            return Ok(model);
        }

        /// <summary>
        /// This method is called when the browser is directed towards /flight/details
        /// Displays the list of passengers on a specific
        /// A javascript file makes the call through fetch
        /// </summary>
        /// <param name="flightId">The specific flight being referenced</param>
        /// <returns>An Ok model containing information for displaying all passengers on the flight</returns>
        [HttpPost("getpassengersforflight")]
        public IActionResult GetForFlight([FromForm] int flightId)
        {
            var flight = _flightRepo.Read(flightId);

            var passengerList = flight?.ListOfPassengers.ToList();

            var model = from pl in passengerList
                        orderby pl.User?.UserName

                        select new
                        {
                            userId = pl.UserID,
                            flightId = pl.FlightID,
                            passengerName = pl.User?.UserName,
                            flightCost = pl.CostOfFlightUSD,
                            flightClass = pl.FlightClass
                        };

            return Ok(model);
        }

        /// <summary>
        /// Called when the browser is pointed to /flightuser/viewusers
        /// A javascript file makes the call through fetch
        /// this method displays all users currently in the system
        /// </summary>
        /// <returns>An Ok model with information for displaying all users in the system</returns>
        [HttpGet("viewusersapi")]
        public IActionResult GetUsersAPI()
        {
            var users = _userRepo.ReadAll();

            var model = from u in users
                        orderby u.Id, u.UserName

                        select new
                        {
                            UserId = u.Id,
                            PassengerName = u.UserName,
                            PassengerPassword = u.Password
                        };

            return Ok(model);
        }

        /// <summary>
        /// Called when the browser is pointed to /flightuser/viewuserreviews
        /// A javascript file makes the call through fetch
        /// Displays all reviews for a specific users
        /// </summary>
        /// <param name="userId">The user being referenced</param>
        /// <returns>An Ok model containing information to display all reviews for that user</returns>
        [HttpPost("viewuserreviewsapi")]
        public IActionResult GetUserReviewsAPI([FromForm] int userId)
        {
            var user = _userRepo.Read(userId);

            var bookedFlights = user?.BookedFlights.ToList();

            var model = from bf in bookedFlights
                        orderby bf.FlightID

                        select new
                        {
                            flightId = bf.FlightID,
                            review = bf.Review
                        };

            return Ok(model);
        }

        /// <summary>
        /// Called when the browser is pointed to /flightuser/updateflightclass
        /// A javascript file makes the call through fetch
        /// Called when the user chooses to submit the form that changes the passengers flight class
        /// Updates the flight class in the database
        /// </summary>
        /// <param name="flightId">The flightId retrieved from the form</param>
        /// <param name="flightUserId">the flightUserId retrieved from the form</param>
        /// <param name="FlightClass">the new flight class value</param>
        /// <returns>NoContent to comply with protocol</returns>
        [HttpPut("changeflightclass")]
        public IActionResult Put([FromForm] int flightId, [FromForm] int flightUserId, [FromForm] string FlightClass)
        {
            _flightUserRepo.UpdateFlightClass(flightUserId, FlightClass);
            return NoContent(); // 204 as per HTTP specification
        }

        /// <summary>
        /// Called when the browser is pointed to /flightuser/updateflightreview
        /// A javascript file makes the call through fetch
        /// Called when the users chooses to submit the form that changes the passengers flight review
        /// Updates the review in the database
        /// </summary>
        /// <param name="flightId">the flightId retrieved from the form</param>
        /// <param name="flightUserId">the flightUserId retrieved from the form</param>
        /// <param name="Review">The new review</param>
        /// <returns>NoContent to comply with protocol</returns>
        [HttpPut("changeflightreview")]
        public IActionResult PutReview([FromForm] int flightId, [FromForm] int flightUserId, [FromForm] string Review)
        {
            Console.WriteLine("changing flight review started");
            _flightUserRepo.UpdateFlightReview(flightUserId, Review);
            return NoContent(); // 204 as per HTTP specification
        }

        /// <summary>
        /// Called when the browser points to /flightuser/create
        /// creates a new flightuser object in the database
        /// </summary>
        /// <param name="flightId">the flightId retrieved from the form</param>
        /// <param name="userId">the userId retrieved from the form</param>
        /// <returns>CreatedAtAction containing information on when it was created</returns>
        [HttpPost("createflightregistration")]
        public IActionResult Post([FromForm] int flightId, [FromForm] int userId)
        {
            var flightUser = _flightUserRepo.Create(flightId, userId);
            flightUser?.Flight?.ListOfPassengers.Clear();
            flightUser?.User?.BookedFlights.Clear();
            Console.WriteLine("new flight registered");
            return CreatedAtAction("Get", new { id = flightUser?.Id }, flightUser);
        }

        /// <summary>
        /// Called when the browser points to /flightuser/remove
        /// Removes a flightuser from the database
        /// </summary>
        /// <param name="flightId">the flightId retrieved from the form</param>
        /// <param name="flightUserId">the flightUserId retrieved from the form</param>
        /// <returns>NoContent to comply with protocol</returns>
        [HttpDelete("removeflightregistration")]
        public IActionResult Delete([FromForm] int flightId, [FromForm] int flightUserId)
        {
            _flightUserRepo.Remove(flightId, flightUserId);
            return NoContent(); // 204 as per HTTP specification
        }
    }
}
