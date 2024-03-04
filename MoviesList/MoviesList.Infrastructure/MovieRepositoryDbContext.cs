using Microsoft.EntityFrameworkCore;
using MoviesList.Domain.Common;
using MoviesList.Domain.Models;

namespace MoviesList.Infrastructure
{
    public class MovieRepositoryDbContext: DbContext
    {
        public MovieRepositoryDbContext(DbContextOptions<MovieRepositoryDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Rating> Ratings {get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entity)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            entity.UpdatedAt = DateTimeOffset.UtcNow;
                            break;
                        case EntityState.Added:
                            entity.CreatedAt = entity.UpdatedAt = DateTimeOffset.UtcNow;
                            break;
                        default:
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
