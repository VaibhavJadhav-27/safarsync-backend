using Dapper;
using Microsoft.AspNetCore.Mvc;
using SafarSync.API.Interfaces;

namespace SafarSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDbConnectionFactory _dbFactory;

        public TestController(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        [HttpGet("ping-db")]
        public async Task<IActionResult> PingDatabase()
        {
            try
            {
                using var connection = _dbFactory.CreateConnection();
                var result = await connection.QueryAsync<string>("SELECT Name FROM USERS");
                return Ok(new { Message = "Database connected successfully!", Result = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database connection failed: {ex.Message}");
            }
        }
    }
}
