namespace NathanielJacksonAssignment2.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;

        public ICollection<FlightUser> BookedFlights { get; set; } = new List<FlightUser>();
    }
}
