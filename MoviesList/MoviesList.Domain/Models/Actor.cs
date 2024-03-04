using MoviesList.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Domain.Models
{
    public class Actor : BaseEntity
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }

        // Navigation property for relationships

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
