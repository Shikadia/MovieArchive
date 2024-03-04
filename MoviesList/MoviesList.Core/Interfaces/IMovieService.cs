using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using static MoviesList.Core.DTOs.Request.UpdateMovieRequest;

namespace MoviesList.Core.Interfaces
{
    public interface IMovieService
    {
        Task<ResponseDto<CreateMovieResponseDTO>> GetMovieById(string movieId);
        Task<ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>> GetAllMovies(int pageNumber, int pageSize);
        Task<ResponseDto<CreateMovieResponseDTO>> CreateMovie(MovieWithActorsDto movieDto);
        Task<ResponseDto<UpdateMovieResponse>> UpdateMovie(string movieId, UpdateMoviesWithActors movieDto);
        Task<ResponseDto<DeleteMovieResponse>> DeleteMovie(string movieId);
        Task<ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>> SearchMovie(string name, int pageNumber, int pageSize);
    }
}
