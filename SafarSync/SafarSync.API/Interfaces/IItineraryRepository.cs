using SafarSync.API.DTOs;
using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface IItineraryRepository
    {
        Task AddItem(ItineraryItem item);
        Task<IEnumerable<ItineraryItem>> GetByTrip(Guid tripId);
        Task UpdateItem(Guid id, UpdateItineraryRequest request);
        Task DeleteItem(Guid id);
    }
}
