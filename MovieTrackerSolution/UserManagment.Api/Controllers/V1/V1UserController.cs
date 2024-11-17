using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V1;
using UserManagment.Api.Extentions;
using UserManagment.Api.Repositoies.Contracts;

namespace UserManagment.Api.Controllers.V1
{
    [Route("api/1.0/users")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entities.User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                var userDto = users.ConvertToDto();

                if (users is not null && userDto is not null)
                {
                    return Ok(new Content
                    {
                        Status = true,
                        data = userDto
                    });
                }

                return NotFound(new Error
                {
                    Status = false,
                    ErrorMessage = "No users to be found"
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retriving data from the database");
            }
        }


        [HttpGet]
        [Route("api/1.0/{id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(string id)
        {
            return null;
        }


    }
}
