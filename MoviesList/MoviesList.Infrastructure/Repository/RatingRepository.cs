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
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        private readonly MovieRepositoryDbContext _dbContext;
        public RatingRepository(MovieRepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Rating> GetAll()
        {
            return _dbContext.Ratings;
        }

        public Task<Rating> GetRatingByIdAsync(string id)
        {
            return _dbContext.Ratings.FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
