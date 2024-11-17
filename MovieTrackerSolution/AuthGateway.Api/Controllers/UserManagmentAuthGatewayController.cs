using AuthGateway.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V2;
using Newtonsoft.Json;

namespace AuthGateway.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagmentAuthGatewayController : ControllerBase
    {

        private readonly IRestClientService _restClient;
        private readonly string _url = "https://localhost:7114";
        public UserManagmentAuthGatewayController(IRestClientService restClient)
        {
            _restClient = restClient;
        }

       
        [HttpGet]
        [Authorize]
        [Route("users/{id}")]
        public async Task<ActionResult<UserReadDto>> GetUserProfileData(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "Missing user id",
                    });
                }



                var response = await _restClient.Get($"{_url}/api/2.0/users/{id}");

                if(response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
                {
                    return StatusCode(StatusCodes.Status404NotFound, new Error
                    {
                        Status = false,
                        ErrorMessage = "User not exist",

                    });
                }

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding user data",
                        ErrorDetails = response.ErrorMessage
                    });

                }

                var userDto = JsonConvert.DeserializeObject<Content<UserReadDto>>(response.Content);

      
                return Ok(userDto);
    
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An unexpected error occurred when finding user",
                    ErrorDetails = ex.ToString()

                });

            }
        } 

    }
}
