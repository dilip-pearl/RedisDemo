using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace RedisDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Home(IDistributedCache cache) : ControllerBase
{
    // Set a value in Redis
    [HttpPost("set/{key}/{value}")]
    public async Task<IActionResult> SetValue(string key, string value)
    {
        var options = new DistributedCacheEntryOptions
        {
            //SlidingExpiration = TimeSpan.FromMinutes(30), // expire if not accessed for 30 minutes
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3) // expire in 3 days
        };

        await cache.SetStringAsync(key, value, options);
        return Ok($"Key '{key}' set with value '{value}'");
    }

    // Get a value from Redis
    [HttpGet("get/{key}")]
    public async Task<IActionResult> GetValue(string key)
    {
        var value = await cache.GetStringAsync(key);
        if (value == null)
            return NotFound($"Key '{key}' not found in Redis");

        return Ok(value);
    }

    // Delete a value from Redis
    [HttpDelete("delete/{key}")]
    public async Task<IActionResult> DeleteValue(string key)
    {
        await cache.RemoveAsync(key);
        return Ok($"Key '{key}' deleted from Redis");
    }
}
