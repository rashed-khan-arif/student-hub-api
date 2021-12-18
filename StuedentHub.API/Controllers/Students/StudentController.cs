using Microsoft.AspNetCore.Mvc;
using StudentHub.API.Controllers.Common;
using StudentHub.Models.Students;
using StudentHub.Repositories.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentHub.API.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseApiController
    {
        private readonly StudentHUBDbContext _context;

        public StudentController(StudentHUBDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var can = _context.Database.CanConnect();

            return Ok(new { connected = can });
        }


        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string value)
        {
            var student = new Student { Id = 1};
           // _context.Students.Add(student);
           // await _context.SaveChangesAsync();
            return Ok(student);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
