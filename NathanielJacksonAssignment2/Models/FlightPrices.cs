namespace NathanielJacksonAssignment2.Models
{
    public static class FlightPrices
    {
        public static Dictionary<string, string> PricesPerClass { get; set; } = new Dictionary<string, string>()
        {
            { "Economy", "$100.00" },
            { "Standard", "$200.00" },
            { "Business", "$300.00" },
            { "VIP", "$500.00" }
        };
    }
}
