using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Response;
using MoviesList.Core.Interfaces;
using MoviesList.Domain.Models;
using System.Net;

namespace MoviesList.Core.Service
{
    public class RatingService : IRateMovie
    {
        private readonly IUnitOfWork _unitOfWork;
        public RatingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<RateMovieResponse>> GetRating(string movieId)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);

                if (movie == null)
                    return ResponseDto<RateMovieResponse>.Fail($"Movie does not exist", (int)HttpStatusCode.BadRequest);

                var rating = movie.Ratings.Average(r => r.Value);

                var result = new RateMovieResponse()
                {
                    AverageRating = rating
                };

                return ResponseDto<RateMovieResponse>.Success($"{movie.Title} has a rating of {result}", result, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ResponseDto<RateMovieResponse>.Fail($"an Error occured", (int)HttpStatusCode.BadRequest);
            }
        }

        public async Task<ResponseDto<RateMovieResponse>> RateMovie(string movieId, int rate)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);
                if (movie == null)
                    return ResponseDto<RateMovieResponse>.Fail($"Movie does not exist", (int)HttpStatusCode.BadRequest);

                var rating = new Rating
                {
                    Value = rate
                };
                movie.Ratings.Add(rating);
                await _unitOfWork.Save();

                var movieUpdated = await _unitOfWork.Movies.GetMovieByIdAsync(movieId);
                var ratingUpdated = movieUpdated.Ratings.Average(r => r.Value);

                var result = new RateMovieResponse()
                {
                    AverageRating = ratingUpdated
                };

                return ResponseDto<RateMovieResponse>.Success($"{movie.Title} has a rating of {result.AverageRating}", result, (int)HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return ResponseDto<RateMovieResponse>.Fail($"an Error occured", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}
