using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using static MoviesList.Core.DTOs.Request.UpdateActorRequest;

namespace MoviesList.Core.Interfaces
{
    public interface IActorService
    {
        Task<ResponseDto<CreateActorResponseDTO>> GetActorById(string actorId);
        Task<ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>> GetAllActors(int pageNumber, int pageSize);
        Task<ResponseDto<CreateActorResponseDTO>> CreateActor(ActorWithMoviesDto actorDto);
        Task<ResponseDto<UpdateActorResponse>> UpdateActor(string actorId, UpdateActorWithMovies actorDto);
        Task<ResponseDto<DeleteActorResponse>> DeleteActor(string actorId);
        Task<ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>> SearchActor(string name, int pageNumber, int pageSize);
    }
}
