using Microsoft.Extensions.DependencyInjection;
using MoviesList.Core.Interfaces;
using MoviesList.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Infrastructure
{
    public static class ServiceExtension
    {
        public static void AddMovieInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
        }
    }
}
