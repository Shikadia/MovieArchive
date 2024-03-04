using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Response
{
    public class CreateMovieResponseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<ActorResponseDTO> Actors { get; set; }
        public double rating { get; set; }
    }

    public class MovieResponseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public double rating { get; set; }

    }

}
