using Microsoft.AspNetCore.Mvc;
using SafarSync.API.DTOs;
using SafarSync.API.Entities;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncUser(SyncUserRequest request)
        {
            var existingUser = await _userRepository.GetByFirebaseId(request.FirebaseUserId);

            if (existingUser != null)
                return Ok(existingUser);

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirebaseUserId = request.FirebaseUserId,
                Name = request.Name,
                Email = request.Email
            };

            var createdUser = await _userRepository.CreateUser(newUser);

            return Ok(createdUser);
        }
    }
}
