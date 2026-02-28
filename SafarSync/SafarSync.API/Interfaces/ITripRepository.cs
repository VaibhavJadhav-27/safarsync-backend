using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> CreateTrip(Trip trip);
        Task AddTripMember(TripMember member);
        Task<IEnumerable<Trip>> GetTripsByUser(Guid userId);
        Task<Trip?> GetTripById(Guid tripId);
    }
}
