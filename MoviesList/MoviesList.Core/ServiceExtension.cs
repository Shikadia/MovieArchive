using Microsoft.Extensions.DependencyInjection;
using MoviesList.Core.Interfaces;
using MoviesList.Core.Service;
using System.Reflection;

namespace MoviesList.Core
{
    public static class ServiceExtension
    {
        public static void AddNothing(this IServiceCollection services) 
        {

        }

    }
}
