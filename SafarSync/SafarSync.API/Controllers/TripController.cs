using Microsoft.AspNetCore.Mvc;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripRepository _tripRepository;

        public TripController(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTrip(CreateTripRequest request)
        {
            if(request == null)
            {
                return BadRequest("Request body is null.");
            }

            if(request.StartDate > request.EndDate)
            {
                return BadRequest("Start date cannot be after end date.");
            }

            var trip = new Trip
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = request.UserId
            };

            var createdTrip = await _tripRepository.CreateTrip(trip);

            var member = new TripMember
            {
                Id = Guid.NewGuid(),
                TripId = createdTrip.Id,
                UserId = request.UserId,
                Role = "Admin"
            };

            await _tripRepository.AddTripMember(member);

            return Ok(createdTrip);
        }

        // GET TRIPS FOR USER
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTripsByUser(Guid userId)
        {
            var trips = await _tripRepository.GetTripsByUser(userId);
            return Ok(trips);
        }

        // GET SINGLE TRIP
        [HttpGet("{tripId}")]
        public async Task<IActionResult> GetTripById(Guid tripId)
        {
            var trip = await _tripRepository.GetTripById(tripId);

            if (trip == null)
                return NotFound();

            return Ok(trip);
        }

        // JOIN TRIP
        [HttpPost("{tripId}/join")]
        public async Task<IActionResult> JoinTrip(Guid tripId, JoinTripRequest request)
        {
            var member = new TripMember
            {
                Id = Guid.NewGuid(),
                TripId = tripId,
                UserId = request.UserId,
                Role = "Member"
            };

            await _tripRepository.AddTripMember(member);

            return Ok("Joined successfully");
        }
    }
}
