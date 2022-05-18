using NathanielJacksonAssignment2.Models.Entities;

namespace NathanielJacksonAssignment2.Services
{
    public interface IFlightUserRepository
    {
        FlightUser? Read(int id);
        ICollection<FlightUser> ReadAll();
        FlightUser? Create(int flightId, int userId);
        void UpdateFlightClass(int flightUserId, string flightClass);
        void UpdateFlightReview(int flightUserId, string Review);
        void Remove(int flightId, int flightUserId);
    }
}
