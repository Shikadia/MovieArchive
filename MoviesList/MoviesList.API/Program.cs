using Microsoft.EntityFrameworkCore;
using MoviesList.Core.Interfaces;
using MoviesList.Core.Service;
using MoviesList.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<MovieRepositoryDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRateMovie, RatingService>();
builder.Services.AddScoped<IdbInitializer, DatabaseInitializer>();
builder.Services.AddMovieInfrastructure();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
seedData();
app.UseCors("AllowAnyOrigin");

app.UseAuthorization();

app.MapControllers();


app.Run();

void seedData()
{
    using var scope = app.Services.CreateScope();
    var dbIntializer = scope.ServiceProvider.GetRequiredService<IdbInitializer>();
    dbIntializer.Initialize().GetAwaiter().GetResult();
}
