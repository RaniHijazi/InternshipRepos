using Microsoft.AspNetCore.Mvc;
using InternshipLibrarie.Interfaces;
using InternshipLibrarie.Models;
using InternshipLibrarie.Dto;

namespace InternshipLibrarie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;

        public UserController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials credentials)
        {
            var user = _userRepository.AuthenticateUser(credentials.Username, credentials.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            try
            {
                var newUser =  _userRepository.AddUser(userDto);
                return Ok(newUser.id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            try
            {
                await _userRepository.DeleteUser(username);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting user: {ex.Message}");
            }
        }


        public class UserCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
