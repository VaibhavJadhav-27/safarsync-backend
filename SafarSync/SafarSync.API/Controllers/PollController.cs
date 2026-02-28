using Microsoft.AspNetCore.Mvc;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    public class PollController : ControllerBase
    {
        private readonly IPollRepository _pollRepository;

        public PollController(IPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll(CreatePollRequest request)
        {
            var poll = new Poll
            {
                Id = Guid.NewGuid(),
                TripId = request.TripId,
                Question = request.Question,
                CreatedBy = request.CreatedBy
            };

            await _pollRepository.AddPoll(poll);

            foreach (var optionText in request.Options)
            {
                var option = new PollOption
                {
                    Id = Guid.NewGuid(),
                    PollId = poll.Id,
                    OptionText = optionText
                };

                await _pollRepository.AddOption(option);
            }

            return Ok(poll);
        }

        [HttpGet("trip/{tripId}")]
        public async Task<IActionResult> GetPolls(Guid tripId)
        {
            var polls = await _pollRepository.GetPollsByTrip(tripId);
            return Ok(polls);
        }

        [HttpPost("{pollId}/vote")]
        public async Task<IActionResult> Vote(Guid pollId, VoteRequest request)
        {
            var vote = new PollVote
            {
                Id = Guid.NewGuid(),
                PollId = pollId,
                OptionId = request.OptionId,
                UserId = request.UserId
            };

            await _pollRepository.AddVote(vote);

            return Ok("Vote recorded");
        }

        [HttpGet("{pollId}/results")]
        public async Task<IActionResult> GetResults(Guid pollId)
        {
            var results = await _pollRepository.GetResults(pollId);
            return Ok(results);
        }
    }
}
