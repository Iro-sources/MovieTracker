using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;
using MovieTracker.Models.V1;
using UserMoviesData.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserMoviesData.Api.Controllers
{
    [Route("api/userMovies")]
    [ApiController]
    public class UserMoviesController : ControllerBase
    {
        private UserMovieDbContext _context;
        public UserMoviesController(UserMovieDbContext context)
        {
            _context = context;
        }

        // GET: api/userMovies/getUserMovies/{userId}
        //Here we are getting the user Movies from Database according to the user who loggedIn
        [HttpGet("getUserMovies/{userId}")]
        public IActionResult getUserMovies(string userId)
        {
            //get users movies
            try
            {
                return Ok(_context.UserMovies.Where(x => x.UserId == userId).ToList());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = e.Message,

                });
            }
        }


        //POST: api/userMovies/addUserMovie
        //here we are adding the movie into database when we click on Add button, if user is logged in then they can add movie 
        [HttpPost("addUserMovie")]
        public IActionResult addUserMovie([FromBody] UserMovies movie)
        {
            try
            {
                var contextMovie = _context.UserMovies.Find(movie.Id);
                if (contextMovie != null)
                {
                    return Ok(false);
                }
                _context.UserMovies.Add(movie);
                _context.SaveChanges();
                return Ok(true);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = e.Message,

                });
            }
        }


        //DELETE api/userMovies/5
        //Here we are deleting the movie when we click on delete button on MyMovies Page
        [HttpDelete("deleteMovie/{movieId}")]
        public IActionResult Delete(string movieId)
        {
            try
            {
                var userMovie = _context.UserMovies.Find(movieId);
                if (userMovie != null)
                {
                    _context.UserMovies.Remove(userMovie);
                    _context.SaveChanges();
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Error
                {
                    Status = false,
                    ErrorMessage = e.Message,

                });
            }
           
        }
    }
}
