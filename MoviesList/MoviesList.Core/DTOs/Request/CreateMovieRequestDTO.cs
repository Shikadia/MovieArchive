using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Request
{
    public class CreateMovieRequestDTO
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
    }
    public class MovieWithActorsDto
    {
        public CreateMovieRequestDTO Movie { get; set; }
        public ICollection<CreateActorRequestDTO> Actors { get; set; }
    }
}
