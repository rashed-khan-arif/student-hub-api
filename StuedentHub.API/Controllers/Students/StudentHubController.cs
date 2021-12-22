using Microsoft.AspNetCore.Mvc;
using StudentHub.API.Controllers.Common;
using StudentHub.Models.Students;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StuedentHub.API.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentHubController : BaseApiController
    {
        // GET: api/<StudentHubController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StudentHubController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StudentHubController>
        [HttpPost]
        public void Post([FromBody] StudentHubModel model)
        {
        }
 
    }
}
