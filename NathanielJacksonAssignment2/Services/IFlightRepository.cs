using NathanielJacksonAssignment2.Models.Entities;

namespace NathanielJacksonAssignment2.Services
{
    public interface IFlightRepository
    {
        Flight Create(Flight newFlight);
        Flight? Read(int id);
        ICollection<Flight> ReadAll();
        void Update(int oldId, Flight flight);
        void Delete(int id);
    }
}
