using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MovieData.Api.configurations;
using MovieData.Api.Entites.AdvanceSearchApi.Models;
using MovieData.Api.Entites.MoviesApi.Models;
using MovieData.Api.Extentions;
using MovieData.Api.Service;
using MovieTracker.Models.V1;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieData.Api.Controllers.V2
{

    [ApiController]
    public class V2MovieController : ControllerBase
    {

        private readonly IRestClientService _restClient;
        private readonly ILogger<V2MovieController> _logger;
        private readonly IOptions<ApiKeyConfiguration> _apiKeyConfiguration;



        public V2MovieController(IRestClientService restClientService,
                ILogger<V2MovieController> logger,
                IOptions<ApiKeyConfiguration> apiKeyConfiguration)
        {
            _restClient = restClientService;
            _logger = logger;
            _apiKeyConfiguration = apiKeyConfiguration;
        }




        [HttpGet]
        [Route("api/2.0/AdvancedSearch")]
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


                _logger.LogTrace("Query string built with parameters: release_date={0}, genres={1}, title={2}", release_date, genres != null ? string.Join(",", genres) : "", title ?? "");

                var response = await _restClient.Get($"/API/AdvancedSearch/{_apiKeyConfiguration.Value.ImdbApiKey}?{queryString}");

                _logger.LogDebug("Response from IMDb API: {0}", response.Content);

                _logger.LogInformation("Movies found with parameters: release_date={0}, genres={1}, title={2}", release_date, genres != null ? string.Join(",", genres) : "", title ?? "");



                if (!response.IsSuccessful)
                {

                    _logger.LogError("Error calling IMDb API. Endpoint: AdvancedSearch, Status code: {0}, Error message: {1}", response.StatusCode, response.ErrorMessage);

                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movies",

                    });
                }

                if(response.Content is not null)
                {
              
                 

                    var data = JsonConvert.DeserializeObject<AdvanceSearchMovies>(response.Content);
                    var movieDto = data.ConvertToDto();
                    return Ok(movieDto);
                }
                // TODO
                return null;
                

             
              

            }
            catch (Exception ex)
            {


                _logger.LogCritical(ex, "An error occurred while calling the IMDb API. Endpoint: AdvancedSearch, {0}", ex.Message);

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
        [Route("api/2.0/movies/{id}")]
        //[Route("{id}")]
        public async Task<ActionResult> GetMovieById([FromRoute] string id)
        {
            try
            {

                //eks https://imdb-api.com/en/API/Title/k_91okrfdi/tt1375666
                var response = await _restClient.Get($"/API/Title/{_apiKeyConfiguration.Value.ImdbApiKey}/{id}");

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
        [Route("api/2.0/topmovies")]
        public async Task<ActionResult> GetTopMovies([FromQuery] int numberOfMovies = 100)
        {
            try
            {
                var response = await _restClient.Get($"/API/Top250Movies/{_apiKeyConfiguration.Value.ImdbApiKey}");

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
        [Route("api/2.0/movies/{id}/youtubetrailer")]
        public async Task<ActionResult> GetMovieTrailer([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/YouTubeTrailer/{_apiKeyConfiguration.Value.ImdbApiKey}/{id}");

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

        //Displays awards that a specifi movie won
        [HttpGet]
        [Route("api/2.0/movies/{id}/movieAwards")]
        public async Task<ActionResult> GetAwardsWonByMovie([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/Awards/{_apiKeyConfiguration.Value.ImdbApiKey}/{id}");

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

        //Displays external sites to stream the movie from
        [HttpGet]
        [Route("api/2.0/movies/{id}/externalSites")]
        public async Task<ActionResult> GetExternalSites([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/ExternalSites/k_91okrfdi/{id}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding external sites",

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
                    ErrorMessage = "An unexpected error occurred when finding external sites",
                    ErrorDetails = ex.Message
                });
            }


        }

        //Displays imdb user movie reviews
        [HttpGet]
        [Route("api/2.0/movies/{id}/reviews")]
        public async Task<ActionResult> GetReviews([FromRoute] string id)
        {
            try
            {
                var response = await _restClient.Get($"/API/Reviews/k_91okrfdi/{id}");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding reviews",

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
                    ErrorMessage = "An unexpected error occurred when finding reviews",
                    ErrorDetails = ex.Message
                });
            }


        }

        //Displays movies in theatre at the moment.
        //To be rendered with time interval as background job
        [HttpGet]
        [Route("api/2.0/InTheaters")]
        public async Task<ActionResult> GetInTheatres()
        {
            try
            {
                var response = await _restClient.Get($"/API/InTheaters/k_91okrfdi");

                if (!response.IsSuccessful)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Error
                    {
                        Status = false,
                        ErrorMessage = "An unexpected error occurred when finding movies",

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
                    ErrorMessage = "An unexpected error occurred when finding movies",
                    ErrorDetails = ex.Message
                });
            }


        }


    }



}
