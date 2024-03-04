using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Request
{
    public class DeleteActorRequest
    {
        public string ActorId { get; set; } = null!;
    }
}
