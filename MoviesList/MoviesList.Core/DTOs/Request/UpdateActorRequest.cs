using MoviesList.Core.DTOs.Response;
using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Request
{
    public class UpdateActorRequest
    {
        public string ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }

     
        public class UpdateActorWithMovies
        {
            public UpdateActorRequest Actor { get; set; }
            public ICollection<CreateMovieRequestDTO> Movies { get; set; }
        }
    }
}

