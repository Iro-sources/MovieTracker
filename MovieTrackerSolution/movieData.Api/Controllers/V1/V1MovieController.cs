using Microsoft.AspNetCore.Mvc;
using MovieData.Api.Entites.AdvanceSearchApi.Models;
using MovieData.Api.Entites.MoviesApi.Models;
using MovieData.Api.Service;
using MovieTracker.Models.V1;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieData.Api.Controllers.V1
{

    [ApiController]
    public class V1MovieController : ControllerBase
    {

        private readonly IRestClientService _restClient;


        public V1MovieController(IRestClientService restClientService)
        {
            _restClient = restClientService;
        }




        [HttpGet]
        [Route("api/1.0/AdvancedSearch")]
        public async Task<ActionResult> GetMoviesByAdvanceSearch(
            [FromQuery] List<string> genres, 
            [FromQuery] string? release_date = "2020", 
            [FromQuery] string? title = null)
        {
            try
            {
                // Building the query string based on the provided parameters
                var queryString = $"release_date={release_date}";

                if (genres is not null && genres.Count > 0)
                {
                    queryString += $"&genres={string.Join(",", genres)}";
                }


                if (!string.IsNullOrWhiteSpace(title))
                {
                    queryString += $"&title={title}";
                }



                var response = await _restClient.Get($"/API/AdvancedSearch/k_91okrfdi?{queryString}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movies",

                    });
                }

                var data = JsonConvert.DeserializeObject<AdvanceSearchMovies>(response.Content);
                return Ok(data);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An unexpected error occurred when finding movies",
                    ErrorDetails = ex.ToString()

                });



            }

        }

        //Get detailed info about a specific film by supplying its unique id
        [HttpGet]
        [Route("api/1.0/movies/{id}")]
        //[Route("{id}")]
        public async Task<ActionResult> GetMovieById([FromRoute] string id)
        {
            try
            {

                //eks https://imdb-api.com/en/API/Title/k_91okrfdi/tt1375666
                var response = await _restClient.Get($"/API/Title/k_91okrfdi/{id}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movie",

                    });
                }

                var data = JsonSerializer.Deserialize<dynamic>(response.Content);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An unexpected error occurred when finding movie",
                    ErrorDetails = ex.Message
                });
            }





        }


        //This Api gets the top 250 movies from imdb and than displays by defaul to the user the top 100
        [HttpGet]
        [Route("api/1.0/topmovies")]
        public async Task<ActionResult> GetTopMovies([FromQuery] int numberOfMovies = 100)
        {
            try
            {
                var response = await _restClient.Get($"/API/Top250Movies/k_91okrfdi");

                if (!response.IsSuccessful || response.Content is null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movies",

                    });
                }


                var data = JsonConvert.DeserializeObject<MovieList>(response.Content);
                data.Items = data.Items.Take(numberOfMovies).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An error occurred while finding the movie data",
                    ErrorDetails = ex.Message
                });
            }




        }


        //This Api lets the user to check the movie trailer in Youtube 
        [HttpGet]
        [Route("api/1.0/youtubetrailer/{id}")]
        public async Task<ActionResult> GetMovieTrailer([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/YouTubeTrailer/k_91okrfdi/{id}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movie trailer",

                    });
                }
                var data = JsonSerializer.Deserialize<dynamic>(response.Content);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An unexpected error occurred when finding movie trailer",
                    ErrorDetails = ex.Message

                });
            }

        }

        //Displays awards that a specific movie won
        [HttpGet]
        [Route("api/1.0/movieAwards/{id}")]
        public async Task<ActionResult> GetAwardsWonByMovie([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/Awards/k_91okrfdi/{id}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movie Awards",

                    });
                }

                var data = JsonSerializer.Deserialize<dynamic>(response.Content);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = "An unexpected error occurred when finding movie Awards",
                    ErrorDetails = ex.Message
                });
            }


        }


    }



}
