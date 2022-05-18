using NathanielJacksonAssignment2.Models.Entities;
namespace NathanielJacksonAssignment2.Services
{
    public interface IUserRepository
    {
        User? Read(int id);
        ICollection<User> ReadAll();
    }
}
