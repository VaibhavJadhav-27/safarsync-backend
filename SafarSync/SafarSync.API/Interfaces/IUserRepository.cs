using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByFirebaseId(string firebaseId);
        Task<User> CreateUser(User user);
    }
}
