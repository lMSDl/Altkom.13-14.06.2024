using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICrudService<User> _service;

        public UsersController(ICrudService<User> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<User> values = await _service.ReadAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User? user = await _service.ReadAsync(id);
            return user == null ? NotFound() : Ok(user);
        }


        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            int userId = await _service.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new { id = userId }, userId);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            if (await _service.ReadAsync(id) == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.ReadAsync(id) == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
