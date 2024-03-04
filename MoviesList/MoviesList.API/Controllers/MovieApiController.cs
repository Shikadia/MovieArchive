using Microsoft.AspNetCore.Mvc;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.Interfaces;
using System.Net.Mime;
using static MoviesList.Core.DTOs.Request.UpdateMovieRequest;

namespace MoviesList.API.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class MovieApiController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IConfiguration _configuration;
        public MovieApiController(IMovieService movieService, IConfiguration configuration)
        {
            _movieService = movieService;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieved movies by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns statusCode 200 if found and statusCode 400 if not found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _movieService.GetMovieById(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Returns all Movies in Pages.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("get_all_movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllMovies(int pageNumber)
        {
            var result = await _movieService.GetAllMovies(Convert.ToInt32(_configuration["ApplicationSettings:PageSize"]), pageNumber);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Adds a movie with given MovieId
        /// </summary>
        /// <param name="request">Data transfer object containing the request parameters</param>
        /// <returns>A data transfer object containing the result of the request</returns>
        [HttpPost("add_movie")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMovie([FromBody] MovieWithActorsDto request)
        {
            var result = await _movieService.CreateMovie(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// searchs a movie with a given name
        /// </summary>
        /// <param name="movieName"></param>
        /// <param name="pageNumber"></param>
        /// <returns>A paginated data transfer object containing the result of the request</returns>
        [HttpPost("search_movie")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMovie([FromBody]string movieName, int pageNumber)
        {
            var result = await _movieService.SearchMovie(movieName, Convert.ToInt32(_configuration["ApplicationSettings:PageSize"]), pageNumber);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// delete a given movie with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns statusCode 200 if successful and statusCode 400 if not</returns>
        [HttpDelete("delet_movie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var result = await _movieService.DeleteMovie(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// updates a given movie with a given id
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="request"></param>
        /// <returns>Returns statusCode 200 if successful, statusCode 400 if not and A data transfer object containing the result of the request</returns>
        [HttpPatch("update_movie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMoviesWithActors request, string movieID)
        {
            var result = await _movieService.UpdateMovie(movieID, request);
            return StatusCode(result.StatusCode, result);
        }

    }
}
