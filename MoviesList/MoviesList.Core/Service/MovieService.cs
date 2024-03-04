using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Request;
using MoviesList.Core.DTOs.Response;
using MoviesList.Core.Interfaces;
using MoviesList.Core.Validator;
using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static MoviesList.Core.DTOs.Request.UpdateMovieRequest;

namespace MoviesList.Core.Service
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MovieService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<CreateMovieResponseDTO>> CreateMovie(MovieWithActorsDto movieDto)
        {
            try
            {
                var existingMovie = await _unitOfWork.Movies.GetMovieByNameAsync(movieDto.Movie.Title.Trim());
                if (existingMovie != null)
                    return ResponseDto<CreateMovieResponseDTO>.Fail($"Movie with name {existingMovie.Title} already exist, try updating the information", (int)HttpStatusCode.BadRequest);

                var movie = new Movie()
                {
                    Title = movieDto.Movie.Title,
                    ReleaseYear = movieDto.Movie.ReleaseYear,       
                    Actors = movieDto.Actors.Select(m => new Actor
                    {
                        Name = (m.FirstName + ", " + m.LastName).ToLower(),
                        BirthYear = m.BirthYear
                    }).ToList(),
                };
                await _unitOfWork.Movies.InsertAsync(movie);
                await _unitOfWork.Save();

                var response = new CreateMovieResponseDTO()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                };
                return ResponseDto<CreateMovieResponseDTO>.Success($"Movie with name {movie.Title} successfully added", response, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ResponseDto<CreateMovieResponseDTO>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);

            }
        }

        public async Task<ResponseDto<DeleteMovieResponse>> DeleteMovie(string movieId)
        {
            try
            {
                var  movie = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return ResponseDto<DeleteMovieResponse>.Fail($"Movie with {movieId} does not exist", (int)HttpStatusCode.BadRequest);

                }
                var response = new DeleteMovieResponse()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                };
                await _unitOfWork.Movies.DeleteAsync(movie);
                await _unitOfWork.Save();

                return ResponseDto<DeleteMovieResponse>.Success($"{response.Title} deleted", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<DeleteMovieResponse>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);

            }
        }

        public async Task<ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>> GetAllMovies(int pageSize, int pageNumber = 1)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;
                var movies = _unitOfWork.Movies.GetAll();
                if (movies == null)
                {
                    return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Fail
                   ("UnSuccessfully", (int)HttpStatusCode.NotFound);
                }
                var response = await movies.OrderByDescending(x => x.Title)
                  .Skip(skip)
                  .Take(pageSize)
                  .ToListAsync();
                var paginationList = response.Select(x => new CreateMovieResponseDTO
                {
                    Title = x.Title,
                    Id = x.Id,
                    Actors = x.Actors.Select(m => new ActorResponseDTO
                    {
                        ActorName = m.Name,
                        ActorId = m.Id
                    }),
                    rating = x.Ratings == null || x.Ratings.Count == 0
                            ? 0
                            : x.Ratings.Average(r => r.Value)
                }).ToList();
                int totalMoviesCount = movies.Count();
                var paginatedResult = new PaginationResult<IEnumerable<CreateMovieResponseDTO>>()
                {
                    PageItems = paginationList,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    NumberOfPages = (int)Math.Ceiling((double)totalMoviesCount / pageSize),
                    PreviousPage = pageNumber - 1
                };

                return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Success
                    ("Successful", paginatedResult, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Fail
                  ("UnSuccessfully", (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<ResponseDto<CreateMovieResponseDTO>> GetMovieById(string movieId)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);
                if (movie == null)
                    return ResponseDto<CreateMovieResponseDTO>.Fail($"Movie does not exist", (int)HttpStatusCode.BadRequest);

                var response = new CreateMovieResponseDTO()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Actors = movie.Actors.Select(m => new ActorResponseDTO
                    {
                        ActorName = m.Name,
                        ActorId = m.Id
                    }),
                    rating = movie.Ratings == null || movie.Ratings.Count == 0
                            ? 0
                            : movie.Ratings.Average(r => r.Value)

                };

                return ResponseDto<CreateMovieResponseDTO>.Success("Successs", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<CreateMovieResponseDTO>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>> SearchMovie(string name, int pageSize = 1, int pageNumber = 1)
        {
            try
            {
                int skip = (pageNumber - 1) * pageSize;
                var movies = _unitOfWork.Movies.GetAll();
                if (movies == null)
                {
                    return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Fail
                   ("UnSuccessfully", (int)HttpStatusCode.NotFound);
                }

                var moviesList = movies.Where(x => x.Title.ToLower().Contains(name.ToLower().Trim()));

                var response = await moviesList.OrderByDescending(x => x.Title)
                  .Skip(skip)
                  .Take(pageSize)
                  .ToListAsync();
                var paginationList = response.Select(x => new CreateMovieResponseDTO
                {
                    Title = x.Title,
                    Id = x.Id,
                     Actors = x.Actors.Select(m => new ActorResponseDTO
                    {
                        ActorName = m.Name,
                        ActorId = m.Id
                    }),
                    rating = x.Ratings == null || x.Ratings.Count == 0
                            ? 0
                            : x.Ratings.Average(r => r.Value)
                }).ToList();
                int totalMoviesCount = moviesList.Count();
                var paginatedResult = new PaginationResult<IEnumerable<CreateMovieResponseDTO>>()
                {
                    PageItems = paginationList,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    NumberOfPages = (int)Math.Ceiling((double)totalMoviesCount / pageSize),
                    PreviousPage = pageNumber - 1
                };
                return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Success
                    ("Successful", paginatedResult, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<PaginationResult<IEnumerable<CreateMovieResponseDTO>>>.Fail
                  ("UnSuccessfully", (int)HttpStatusCode.NotFound);
            }
        }

        public async Task<ResponseDto<UpdateMovieResponse>> UpdateMovie(string movieId, UpdateMoviesWithActors movieDto)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);

                if (movie == null)
                    return ResponseDto<UpdateMovieResponse>.Fail($"Movie not found", (int)HttpStatusCode.BadRequest);
                movie.Title = movieDto.movies.Title;
                movie.ReleaseYear = movieDto.movies.ReleaseYear;
                foreach (var actors in movieDto.actors)
                {
                    var existingactor = movie.Actors.FirstOrDefault(em => em.Name == (actors.FirstName + ", " + actors.LastName).ToLower());

                    if (existingactor != null)
                    {
                        // Update existing movie
                        existingactor.Name = (actors.FirstName + ", " + actors.LastName).ToLower();
                        existingactor.BirthYear = existingactor.BirthYear;
                    }
                    else
                    {
                        // Add new movie
                        movie.Actors.Add(new Actor
                        {
                            Name = (actors.FirstName + ", " + actors.LastName).ToLower(),
                            BirthYear = actors.BirthYear
                        });
                    }
                }

                _unitOfWork.Movies.Update(movie);

                var updateMovie = new UpdateMovieResponse()
                {
                    Title = movie.Title,
                    ReleaseYear = movie.ReleaseYear,
                };

                var response = _mapper.Map<UpdateMovieResponse>(updateMovie);
                await _unitOfWork.Save();

                return ResponseDto<UpdateMovieResponse>.Success($"{response.Title} updated", response, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResponseDto<UpdateMovieResponse>.Fail($"An Error occured {ex.Message}", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}
