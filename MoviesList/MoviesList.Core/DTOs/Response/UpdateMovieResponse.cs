using MoviesList.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Response
{
    public class UpdateMovieResponse
    {
        public string MovieId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
    }
}
