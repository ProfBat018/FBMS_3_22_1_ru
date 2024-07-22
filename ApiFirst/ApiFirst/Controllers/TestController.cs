using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFirst.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetString()
        {
            return Ok("Hello, World!");
        }

        [HttpGet("{id}")]
        public IActionResult GetString(int id)
        {
            return Ok($"Hello, World! Your id is {id}");
        }

        [HttpPost]
        public IActionResult PostString([FromBody] string value)
        {
            return Ok($"Hello, World! You sent: {value}");
        }
    }
}
