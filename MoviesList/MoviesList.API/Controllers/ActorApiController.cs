using Microsoft.AspNetCore.Mvc;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using MoviesList.Core.Interfaces;
using System.Net.Mime;
using static MoviesList.Core.DTOs.Request.UpdateActorRequest;

namespace MoviesList.API.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ActorApiController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IConfiguration _configuration;
        public ActorApiController(IActorService actorService, IConfiguration configuration)
        {
            _actorService = actorService;
            _configuration = configuration;
        }

        /// <summary>
        /// Retrieved actors by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns statusCode 200 if found and statusCode 400 if not found</returns>
        [HttpGet("get_actor_by_id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _actorService.GetActorById(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Returns all Movies in Pages.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet("get_all_actors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllActors(int pageNumber)
        {
            var result = await _actorService.GetAllActors(Convert.ToInt32(_configuration["ApplicationSettings:PageSize"]), pageNumber);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Adds a actor with given MovieId
        /// </summary>
        /// <param name="request">Data transfer object containing the request parameters</param>
        /// <returns>A data transfer object containing the result of the request</returns>
        [HttpPost("add_actor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddActor([FromBody] ActorWithMoviesDto request)
        {
            var result = await _actorService.CreateActor(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// searchs a movie with a given name
        /// </summary>
        /// <param name="actorName"></param>
        /// <param name="pageNumber"></param>
        /// <returns>A paginated data transfer object containing the result of the request</returns>
        [HttpPost("search_actor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchForActors([FromBody] string actorName, int pageNumber)
        {
            var result = await _actorService.SearchActor(actorName, Convert.ToInt32(_configuration["ApplicationSettings:PageSize"]), pageNumber);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// delete a given actor with a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns statusCode 200 if successful and statusCode 400 if not</returns>
        [HttpDelete("delete_actor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActor(string id)
        {
            var result = await _actorService.DeleteActor(id);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// updates a given movie with a given id
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="request"></param>
        /// <returns>Returns statusCode 200 if successful, statusCode 400 if not and A data transfer object containing the result of the request</returns>
        [HttpPatch("update_actor/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateActor([FromBody] UpdateActorWithMovies request, string movieID)
        {
            var result = await _actorService.UpdateActor(movieID, request);
            return StatusCode(result.StatusCode, result);
        }
    }
}

