using MoviesList.Core.DTOs.Request;
using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Response
{
    public class UpdateActorResponse
    {
        public string ActorId { get; set; }
        public string Name { get; set; }
        public int BirthYear { get; set; }
    }
}
