using SafarSync.API.Entities;

namespace SafarSync.API.Interfaces
{
    public interface IPollRepository
    {
        Task AddPoll(Poll poll);
        Task AddOption(PollOption option);
        Task<IEnumerable<Poll>> GetPollsByTrip(Guid tripId);
        Task AddVote(PollVote vote);
        Task<IEnumerable<(Guid OptionId, int VoteCount)>> GetResults(Guid pollId);
    }
}
