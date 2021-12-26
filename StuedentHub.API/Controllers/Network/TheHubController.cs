using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuedentHub.API.Controllers.Network
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TheHubController : ControllerBase
    {
        // GET: api/<TheHubController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(new string[] { "value1", "value2" });
        }

        // GET api/<TheHubController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TheHubController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TheHubController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
