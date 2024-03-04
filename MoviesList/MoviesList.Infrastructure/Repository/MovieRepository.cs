using Microsoft.EntityFrameworkCore;
using MoviesList.Core.Interfaces;
using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Infrastructure.Repository
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly MovieRepositoryDbContext _dbContext;
        public MovieRepository(MovieRepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Movie>GetAll()
        {
            return _dbContext.Movies;
        }

        public async Task<Movie>GetMovieByIdAsync(string id)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Movie>GetMovieByNameAsync(string name)
        {
            return await _dbContext.Movies.FirstOrDefaultAsync(x => x.Title == name);
        }
    }
}
