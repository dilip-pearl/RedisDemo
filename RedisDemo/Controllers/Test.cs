using Microsoft.AspNetCore.Mvc;

namespace RedisDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test controller is working!");
    }
}
