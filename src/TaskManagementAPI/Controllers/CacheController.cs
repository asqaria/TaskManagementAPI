using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Services;
using System.Threading.Tasks;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/cache")]
    public class CacheController : ControllerBase
    {
        private readonly RedisService redisService;

        public CacheController(RedisService redisService)
        {
            this.redisService = redisService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetCache(string key)
        {
            var value = await redisService.GetAsync(key);
            if (value == null)
            {
                return NotFound("Not Found");
            }
            return Ok(new { key, value });
        }

    }
}