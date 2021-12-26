using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentHub.API.Controllers.Common;
using StudentHub.Models.Common;
using StudentHub.Models.Students;
using StudentHub.Repositories.Core;
using StudentHub.Supervisor.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentHub.API.Controllers.Students
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var all = await _service.GetAll();

            return Ok(all);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);
            if (response == null) return NotFound();
            return Ok(response.Convert());
        }


        [HttpPost("create-profile")]
        public async Task<IActionResult> Post([FromBody] Student body)
        {
            var result = await _service.Insert(body);
            if (result == null) return BadRequest();
            return Created("/",body);
           
        }
    }
}
