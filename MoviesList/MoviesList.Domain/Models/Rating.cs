using MoviesList.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Domain.Models
{
    public class Rating : BaseEntity
    {
        public int Value { get; set; }

        // Foreign keys for relationships
        public int MovieId { get; set; }

        // Navigation properties for relationships
        public virtual Movie Movie { get; set; }
    }
}
