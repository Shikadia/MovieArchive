using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Response
{
    public class ActorResponseDTO
    {
        public string ActorId { get; set;}
        public string ActorName { get; set;}
    }

    public class CreateActorResponseDTO
    {
        public string ActorId { get; set; }
        public string ActorName { get; set; }
        public IEnumerable<MovieResponseDTO> Movies { get; set; }
    }
}
