using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using MoviesList.Core.Interfaces;
using MoviesList.Domain.Models;
using System.Net;
using static MoviesList.Core.DTOs.Request.UpdateActorRequest;

namespace MoviesList.Core.Service
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<CreateActorResponseDTO>> CreateActor(ActorWithMoviesDto actorDto)
        {
            try
            {
                var actorName = actorDto.Actor.FirstName + ", " + actorDto.Actor.LastName;
                var existingActor = await _unitOfWork.Actors.GetActorsByNameAsync(actorName.Trim().ToLower());
                if (existingActor != null)
                    return ResponseDto<CreateActorResponseDTO>.Fail($"Movie with name {existingActor.Name} already exist, try updating the information", (int)HttpStatusCode.BadRequest);

                var actor = new Actor()
                {
                    Name = (actorDto.Actor.FirstName + ", " + actorDto.Actor.LastName).ToLower(),
                    BirthYear = actorDto.Actor.BirthYear,
                    Movies = actorDto.Movies.Select(m => new Movie
                    {
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear
                    }).ToList(),
                };
                await _unitOfWork.Actors.InsertAsync(actor);
                await _unitOfWork.Save();

                var response = new CreateActorResponseDTO()
                {
                    ActorId = actor.Id,
                    ActorName = actor.Name,
                };
                return ResponseDto<CreateActorResponseDTO>.Success($"Movie with name {response.ActorName} successfully added", response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<CreateActorResponseDTO>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);

            }
        }

        public async Task<ResponseDto<DeleteActorResponse>> DeleteActor(string actorId)
        {
            try
            {
                var actor = await _unitOfWork.Actors.GetActorByIdAsync(actorId);
                if (actor == null)
                {
                    return ResponseDto<DeleteActorResponse>.Fail($"Actor with {actorId} does not exist", (int)HttpStatusCode.BadRequest);

                }
                var response = new DeleteActorResponse()
                {
                    ActorName = actor.Name,
                    Id = actor.Id
                };
                await _unitOfWork.Actors.DeleteAsync(actor);

                return ResponseDto<DeleteActorResponse>.Success($"{response.ActorName} deleted", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<DeleteActorResponse>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);

            }
        }

        public async Task<ResponseDto<CreateActorResponseDTO>> GetActorById(string actorId)
        {
            try
            {
                var actor = await _unitOfWork.Actors.GetActorByIdAsync(actorId);
                if (actor == null)
                    return ResponseDto<CreateActorResponseDTO>.Fail($"Movie does not exist", (int)HttpStatusCode.BadRequest);

                var response = new CreateActorResponseDTO()
                {
                    ActorName = actor.Name,
                    ActorId = actor.Id,
                    Movies = actor.Movies.Select(m => new MovieResponseDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        rating = m.Ratings == null || m.Ratings.Count == 0
                            ? 0
                            : m.Ratings.Average(r => r.Value)
                    })
                };

                return ResponseDto<CreateActorResponseDTO>.Success("Successs", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<CreateActorResponseDTO>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>> GetAllActors(int pageSize, int pageNumber = 1)
        {

            try
            {
                int skip = (pageNumber - 1) * pageSize;
                var actors = _unitOfWork.Actors.GetAll();
                if (actors == null)
                {
                    return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Fail
                   ("UnSuccessfully", (int)HttpStatusCode.NotFound);
                }
                var response = await actors.OrderByDescending(x => x.Id)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();

                int totalactorsCount = actors.Count();

                var paginationList = response.Select(x => new CreateActorResponseDTO
                {
                    ActorId = x.Id,
                    ActorName = x.Name,
                    Movies = x.Movies.Select(m => new MovieResponseDTO
                    {
                        Id = m.Id == null ? "" : m.Id,
                        Title = m.Title == null ? "" : m.Title,
                        rating = m.Ratings == null || m.Ratings.Count == 0
                            ? 0
                            : m.Ratings.Average(r => r.Value)
                                    }),
                }).ToList();
                var page = new PaginationResult<IEnumerable<CreateActorResponseDTO>>()
                {
                    PageItems = paginationList,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    NumberOfPages = (int)Math.Ceiling((double)totalactorsCount / pageSize),
                    PreviousPage = pageNumber - 1
                };

                return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Success
                    ("Successful", page, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Fail
                  ("UnSuccessfull", (int)HttpStatusCode.NotFound);
            }
        }
        public async Task<ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>> SearchActor(string name, int pageSize, int pageNumber = 1)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;
                var actors = _unitOfWork.Actors.GetAll();
                if (actors == null)
                {
                    return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Fail
                   ("UnSuccessfully", (int)HttpStatusCode.NotFound);
                }

                var actorsList = actors.Where(x => x.Name.ToLower().Contains(name.ToLower().Trim()));
                int totalactorsCount = actorsList.Count();

                var response = await actorsList.OrderByDescending(x => x.Name)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();
                var paginationList = response.Select(x => new CreateActorResponseDTO
                {
                    ActorId = x.Id,
                    ActorName = x.Name,
                    Movies = x.Movies.Select(m => new MovieResponseDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        rating = m.Ratings == null || m.Ratings.Count == 0
                            ? 0
                            : m.Ratings.Average(r => r.Value)
                    }),
                }).ToList();
                var paginatedResult = new PaginationResult<IEnumerable<CreateActorResponseDTO>>()
                {
                    PageItems = paginationList,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    NumberOfPages = (int)Math.Ceiling((double)totalactorsCount / pageSize),
                    PreviousPage = pageNumber - 1
                };
                return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Success
                    ("Successful", paginatedResult, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PaginationResult<IEnumerable<CreateActorResponseDTO>>>.Fail
                  ("UnSuccessfully", (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<ResponseDto<UpdateActorResponse>> UpdateActor(string actorId, UpdateActorWithMovies actorDto)
        {
            try
            {
                var actors = await _unitOfWork.Actors.GetActorByIdAsync(actorId);

                if (actors == null)
                    return ResponseDto<UpdateActorResponse>.Fail($"Movie not found", (int)HttpStatusCode.BadRequest);


                actors.Name = (actorDto.Actor.FirstName + ", " + actorDto.Actor.LastName).ToLower();
                actors.BirthYear = actorDto.Actor.BirthYear;
                foreach (var newMovieDto in actorDto.Movies)
                {
                    var existingMovie = actors.Movies.FirstOrDefault(em => em.Title == newMovieDto.Title);

                    if (existingMovie != null)
                    {
                        // Update existing movie
                        existingMovie.Title = newMovieDto.Title;
                        existingMovie.ReleaseYear = newMovieDto.ReleaseYear;
                    }
                    else
                    {
                        // Add new movie
                        actors.Movies.Add(new Movie
                        {
                            Title = newMovieDto.Title,
                            ReleaseYear = newMovieDto.ReleaseYear
                        });
                    }
                }

                _unitOfWork.Actors.Update(actors);

                var response = new UpdateActorResponse()
                {
                    ActorId = actorId,
                    Name = (actorDto.Actor.FirstName + ", " + actorDto.Actor.LastName).ToLower(),
                    BirthYear = actorDto.Actor.BirthYear
                };
                await _unitOfWork.Save();

                return ResponseDto<UpdateActorResponse>.Success($"{response.Name} updated", response, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResponseDto<UpdateActorResponse>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}
