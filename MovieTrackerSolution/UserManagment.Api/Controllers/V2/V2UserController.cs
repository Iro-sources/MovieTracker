using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V2;
using UserManagment.Api.Entities;
using UserManagment.Api.Extentions;
using UserManagment.Api.Repositoies;
using UserManagment.Api.Repositoies.Contracts;

namespace UserManagment.Api.Controllers.V2
{
    [Route("api/2.0/users")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                var userDto = users.ConvertToDto();

                if (users is not null && userDto is not null)
                {
                    return Ok(new Content<IEnumerable<UserReadDto>>(userDto));
                }

                return NotFound(new Error
                {
                    Status = false,
                    ErrorMessage = "No users to be found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "Error retriving user data from the database",
                    ErrorDetails = ex.Message,
                });

            }
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<UserReadDto>> GetUser(string id)
        {
            try
            {
                User? user = await _userRepository.GetUserById(id);

                if (user is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Error
                    {
                        Status = false,
                        ErrorMessage = "User not exist",

                    });

                }
                else
                {
                    var userDto = user.ConvertToDto();

                    return StatusCode(StatusCodes.Status200OK, new Content<UserReadDto>(userDto));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "Error retriving user from the database",
                    ErrorDetails = ex.Message,
                });
            }
        }

       
        [HttpPost]
        [Route("{userId}/config/genrePreference")]
        public async Task<ActionResult> UpdateUserGenrePreference(string userId, Dictionary<string, bool> preferences)
        {
            try
            {
                User? user = await _userRepository.GetUserById(userId);
                if (user is null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Error
                    {
                        Status = false,
                        ErrorMessage = "User not exist",

                    });

                }

                if(preferences is null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "movie genre preferences is null",

                    });
                }

               await _userRepository.UpdateUserGenrePreference(userId, preferences);

     

             return StatusCode(StatusCodes.Status200OK, new Content<string>("user genre preferences updated"));

            }
            catch
            (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "Error updating user genre preferences",
                    ErrorDetails = ex.Message,
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult<UserSignUpDto>> CreateUser([FromBody] UserSignUpDto userSignUpDto)
        {
            try
            {
              if (string.IsNullOrEmpty(userSignUpDto.Email) ||
              string.IsNullOrEmpty(userSignUpDto.FirstName) ||
              string.IsNullOrEmpty(userSignUpDto.LastName) ||
              string.IsNullOrEmpty(userSignUpDto.Password) ||
              !userSignUpDto.GenreConfig.Any())
              {

                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "You must provide a first name, lastName, Email, password, and at least one genre.",

                    });

                }

              var existingUser = await _userRepository.GetUserByEmail(userSignUpDto.Email);

              if (existingUser is not null) 
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "Email already in use!",

                    });
                }


              var user = await _userRepository.CreateNewUser(userSignUpDto);

               if(user != null)
               {
                    return Ok(new Content<UserSignUpDto>(user));
               }
               else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "something was wrong",
                    });
                }
               


            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "Error creating new user in the database",
                    ErrorDetails = ex.Message,
                });
            }
        }
    }
}
