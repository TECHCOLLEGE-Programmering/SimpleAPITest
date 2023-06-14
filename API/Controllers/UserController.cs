using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // A logger object for loging this Controller
        private readonly ICustomService<User> _service;
        private readonly ILogger<UserController> _logger; //TODO how to use logger
        public UserController(ILogger<UserController> logger, ICustomService<User> service)
        {
            _service = service;
            _logger = logger;
        }
        // GET: api/<UserController>
        [HttpGet(Name = "GetUsers")]
        public ActionResult<IEnumerable<User>> Get([FromQuery] string? name, [FromQuery] string? email)
        {
            var users = _service.GetAll(name, email);
            if (users == null)
                return NotFound();
            return Ok(users);
        }

        // GET api/<UserController>
        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _service.Get(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // PUT api/<UserController>
        [HttpPut(Name = "PutUser")]
        public ActionResult<User> Put([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            User UpdatedUser = _service.Update(user);

            if (UpdatedUser == null)
                return BadRequest();

            return Ok(UpdatedUser);
        }

        // POST api/<UserController>
        [HttpPost(Name = "PostUser")]
        public ActionResult<User> Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            User InsertedUser = _service.Insert(user);

            if (InsertedUser == null)
                return BadRequest();

            return Ok(InsertedUser);
        }

        // DELETE api/<UserController>
        [HttpDelete("{id}", Name = "DeleteUser")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("User was deleted.");
        }
    }
}
