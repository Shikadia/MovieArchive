using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActorRepository Actors {get; }
        IMovieRepository Movies {get; }
        IRatingRepository Ratings {get; }
        Task Save();
    }
}
