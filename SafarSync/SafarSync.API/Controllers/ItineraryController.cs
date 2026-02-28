using Microsoft.AspNetCore.Mvc;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItineraryController : ControllerBase
    {
        private readonly IItineraryRepository _itineraryRepository;

        public ItineraryController(IItineraryRepository itineraryRepository)
        {
            _itineraryRepository = itineraryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItineraryRequest request)
        {
            var item = new ItineraryItem
            {
                Id = Guid.NewGuid(),
                TripId = request.TripId,
                DayNumber = request.DayNumber,
                Title = request.Title,
                Description = request.Description,
                Time = request.Time,
                CreatedBy = request.CreatedBy
            };

            await _itineraryRepository.AddItem(item);

            return Ok(item);
        }

        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetTripItinerary(Guid tripId)
        {
            var items = await _itineraryRepository.GetByTrip(tripId);
            return Ok(items);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateItineraryRequest request)
        {
            await _itineraryRepository.UpdateItem(id, request);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itineraryRepository.DeleteItem(id);
            return Ok("Deleted");
        }
    }
}
