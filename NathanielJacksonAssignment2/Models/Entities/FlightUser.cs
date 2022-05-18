namespace NathanielJacksonAssignment2.Models.Entities
{
    public class FlightUser
    {
        public int Id { get; set; }
        public string CostOfFlightUSD { get; set; } = "$100.00";
        public string FlightClass { get; set; } = "Economy";
        public string Review { get; set; } = "No review written yet";


        public int FlightID { get; set; }
        public Flight? Flight { get; set; } 
        public int UserID { get; set; }
        public User? User { get; set; }
    }
}
