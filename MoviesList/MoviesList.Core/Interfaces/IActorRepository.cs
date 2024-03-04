using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Interfaces
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        Task<Actor> GetActorByIdAsync(string id);
        IQueryable<Actor> GetAll();
        Task<Actor> GetActorsByNameAsync(string name);
    }
}
