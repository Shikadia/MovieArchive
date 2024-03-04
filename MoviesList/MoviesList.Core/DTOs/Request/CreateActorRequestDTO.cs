using MoviesList.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Request
{
    public class CreateActorRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
    }

    public class ActorWithMoviesDto
    {
        public CreateActorRequestDTO Actor { get; set; }
        public ICollection<CreateMovieRequestDTO> Movies { get; set; }
    }
}
