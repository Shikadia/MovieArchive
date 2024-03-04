using MoviesList.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly MovieRepositoryDbContext _dbContext;
        private MovieRepository _movieRepository { get; set; } = null!;
        private ActorRepository _actorRepository { get; set; } = null!;
        private RatingRepository _ratingRepository { get; set; } = null!;
        public UnitOfWork(MovieRepositoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActorRepository Actors => _actorRepository ?? new ActorRepository(_dbContext);

        public IMovieRepository Movies => _movieRepository ?? new MovieRepository(_dbContext);

        public IRatingRepository Ratings => _ratingRepository ?? new RatingRepository(_dbContext);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
