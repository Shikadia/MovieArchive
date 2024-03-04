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
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        private readonly MovieRepositoryDbContext _dbContext;
        public ActorRepository( MovieRepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Actor> GetAll()
        {
            return _dbContext.Actors;
        }

        public async Task<Actor> GetActorByIdAsync(string id)
        {
            return await _dbContext.Actors.FirstOrDefaultAsync(x=> x.Id == id);
        }
        public async Task<Actor> GetActorsByNameAsync(string name)
        {
            return await _dbContext.Actors.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
