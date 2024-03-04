using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.DTOs.Request
{
    public class RateMovieRequest
    {
        public string MovieId {  get; set; }
        public int Value { get; set; }
    }
}
