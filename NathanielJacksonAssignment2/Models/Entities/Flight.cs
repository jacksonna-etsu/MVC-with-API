namespace NathanielJacksonAssignment2.Models.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string Destination { get; set; } = String.Empty;
        public string StartLocation { get; set; } = String.Empty;
        public string ArrivalTime { get; set; } = String.Empty;
        public string DepartureTime { get; set; } = String.Empty;

        public ICollection<FlightUser> ListOfPassengers { get; set; } = new List<FlightUser>();
    }
}
