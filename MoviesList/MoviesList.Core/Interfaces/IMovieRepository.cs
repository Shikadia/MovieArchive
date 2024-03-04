using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(string id);
        IQueryable<Movie> GetAll();
        Task<Movie> GetMovieByNameAsync(string name);
    }
}
