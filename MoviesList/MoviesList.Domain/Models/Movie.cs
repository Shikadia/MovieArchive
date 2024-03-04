using MoviesList.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MoviesList.Domain.Models
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }

        // Navigation property for relationships
        
        public virtual ICollection<Actor> Actors { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
