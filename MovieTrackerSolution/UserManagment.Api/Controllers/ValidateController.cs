using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V1;
using UserManagment.Api.Extentions;
using UserManagment.Api.Repositoies.Contracts;

namespace UserManagment.Api.Controllers
{  

    [Route("api/auth")]
    [ApiController]
    public class ValidateController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public ValidateController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("validate")]
        public async Task<ActionResult<UserReadDto>> ValidateLoginDetails([FromBody] LoginRequest loginRequest)
        {
            // input validating
            if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new Error
                {
                    Status = false,
                    ErrorMessage = "All input is required"
                });
            }

            var user = await _userRepository.GetUserByEmail(loginRequest.Email);



            if (user is null || user.Password.ToLower() != loginRequest.Password.ToLower())
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Error
                {
                    Status = false,
                    ErrorMessage = "Wrong email or passowrd"
                });
            }


            // successfuly validated and convert user to dto object
            var userDto = user.ConvertToDto();


            return userDto;
        }
    }
}
