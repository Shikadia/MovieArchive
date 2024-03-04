using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<Rating> GetRatingByIdAsync(string id);
        IQueryable<Rating> GetAll();
    }
}
