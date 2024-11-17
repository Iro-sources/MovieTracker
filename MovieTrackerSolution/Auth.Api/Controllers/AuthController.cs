using Auth.Api.Auth;
using Auth.Api.Services;
using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models.Dtos.UserManagmentAndAuth.Dtos;
using MovieTracker.Models.V1;
using Newtonsoft.Json;

namespace Auth.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IRestClientService _restClient;

        public AuthController(IRestClientService restClientService)
        {
            _restClient = restClientService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                // input validating
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Error
                    {
                        Status = false,
                        ErrorMessage = "All inputs are required",

                    });
                }


                // Validate user
                var response = await _restClient.Post("/api/auth/validate", loginRequest);


                if (response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new Error
                    {
                        Status = false,
                        ErrorMessage = "Wrong email or passowrd",
                        ErrorDetails = response.ErrorMessage
                    });
                }

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding user",
                        ErrorDetails = response.ErrorMessage
                    });

                }

                var userDto = JsonConvert.DeserializeObject<UserReadDto>(response.Content);

                //generate auth token
                var loginRespone = userDto.GenerateJwtToken();


                return loginRespone;



            }
            catch (Exception ex)
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
