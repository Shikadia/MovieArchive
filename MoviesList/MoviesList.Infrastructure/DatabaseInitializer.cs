using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesList.Core.Interfaces;
using MoviesList.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Infrastructure
{
    public class DatabaseInitializer : IdbInitializer
    {
        private readonly MovieRepositoryDbContext _dbContext;
        public DatabaseInitializer(MovieRepositoryDbContext dbContext)
        {
                _dbContext = dbContext;
        }
        public async Task Initialize()
        {
            //await _context.Database.EnsureCreatedAsync();
            try
            {
                if (!_dbContext.Movies.Any())
                {
                    List<Movie> movies = new List<Movie>
                    {
                       new Movie
                       {
                           Title = "The Shawshank Redemption",
                           ReleaseYear = 1994,
                           Actors = new List<Actor>
                           {
                               new Actor {Name = "Tim, Robbins", BirthYear = 1958},
                               new Actor {Name = "Morgan, Freeman", BirthYear = 1937}
                           },
                           Ratings = new List<Rating>
                           {
                               new Rating {Value = 5},
                               new Rating {Value = 4},
                               new Rating {Value = 5},
                               new Rating {Value = 3},

                           }
                       },
                       new Movie
                       {
                           Title = "Avatar ",
                           ReleaseYear = 2009,
                           Actors = new List<Actor>
                           {
                               new Actor {Name = "Sam, Worthington", BirthYear = 1976},
                               new Actor {Name = "Zoe, Saldana ", BirthYear = 1978}
                           },
                           Ratings = new List<Rating>
                           {
                               new Rating {Value = 5},
                               new Rating {Value = 4},
                               new Rating {Value = 5},
                               new Rating {Value = 3},

                           }
                       },
                       new Movie
                       {
                           Title = "The Godfather ",
                           ReleaseYear = 1972,
                           Actors = new List<Actor>
                           {
                               new Actor {Name = "Marlon, Brando", BirthYear = 1924},
                               new Actor {Name = "Al, Pacino", BirthYear = 1940}
                           },
                           Ratings = new List<Rating>
                           {
                               new Rating {Value = 5},
                               new Rating {Value = 4},
                               new Rating {Value = 5},
                               new Rating {Value = 5},

                           }
                       }, 
                       new Movie
                       {
                           Title = "The Matrix",
                           ReleaseYear = 1999,
                           Actors = new List<Actor>
                           {
                               new Actor {Name = "Keanu, Reeves", BirthYear = 1964},
                               new Actor {Name = "Laurence, Fishburne", BirthYear = 1961}
                           },
                           Ratings = new List<Rating>
                           {
                               new Rating {Value = 5},
                               new Rating {Value = 4},
                               new Rating {Value = 5},
                               new Rating {Value = 4},
                           }
                       }
                    };

                    await _dbContext.Movies.AddRangeAsync(movies);
                    await _dbContext.SaveChangesAsync();

                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
