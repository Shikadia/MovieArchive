using MoviesList.Core.DTOs;
using MoviesList.Core.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Interfaces
{
    public interface IRateMovie
    {
        Task<ResponseDto<RateMovieResponse>> GetRating(string movieId);
        Task<ResponseDto<RateMovieResponse>> RateMovie(string movieId, int rate);
    }
}
