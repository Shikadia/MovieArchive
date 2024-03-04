using Microsoft.AspNetCore.Mvc;
using MoviesList.Core.Interfaces;
using MoviesList.Core.Service;
using System.Net.Mime;

namespace MoviesList.API.Controllers
{

    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRateMovie _rateMovie;
        private readonly IConfiguration _configuration;
        public RatingsController(IRateMovie rateMovie, IConfiguration configuration)
        {
            _configuration = configuration;
            _rateMovie = rateMovie;
        }

        /// <summary>
        /// Retrieved the rating of a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns statusCode 200 if found and statusCode 400 if not found</returns>
        [HttpGet("get_rating/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _rateMovie.GetRating(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// rate of a movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rate"></param>
        /// <returns>Returns statusCode 200 if successfull and statusCode 400 if not</returns>
        [HttpGet("rate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RateMovie(string id, int rate)
        {
            var result = await _rateMovie.RateMovie(id, rate);
            return StatusCode(result.StatusCode, result);
        }
    }
}
