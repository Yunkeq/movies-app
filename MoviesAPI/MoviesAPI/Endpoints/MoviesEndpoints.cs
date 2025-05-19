using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Entities;

namespace MoviesAPI.Endpoints
{
    public static class MoviesEndpoints
    {
        public static RouteGroupBuilder MapMoviesEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/movies");

            // GET/Movies
            group.MapGet("/", async (MovieContext movieContext) =>
            await movieContext.Movies
            .AsNoTracking()
            .Include(m => m.Genre)
            .ToListAsync());

            //GET/Movies{id}
            group.MapGet("/{id}", async (MovieContext movieContext, int id) =>
            {
                var movie = await movieContext.Movies
                .AsNoTracking()
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
                return movie is null ? Results.NotFound() : Results.Ok(movie);
            });

            //POST/Movies
            group.MapPost("/", async (MovieContext movieContext, Movie newMovie) =>
            {
                if (!await movieContext.Movies.AsNoTracking().AnyAsync(m => m.Name == newMovie.Name && m.ReleaseDate == newMovie.ReleaseDate))
                {
                    newMovie.Genre = await movieContext.Genres.FirstOrDefaultAsync(g => g.Id == newMovie.GenreId);
                    await movieContext.AddAsync(newMovie);
                    await movieContext.SaveChangesAsync();
                    return Results.Created($"/movies/{newMovie.Id}", newMovie);
                }
                return Results.Conflict(new { mesage = "This object already exist in database." });
            });

            //PUT/Movies/{id}
            group.MapPut("/{id}", async (MovieContext movieContext, Movie updatedMovie, int id) =>
            {
                Movie? movie = await movieContext.Movies.FindAsync(id);
                if (movie == null)
                {
                    return Results.NotFound();
                }

                await movieContext.Movies
                .Where(m => m.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(m => m.Name, updatedMovie.Name)
                .SetProperty(m => m.Price, updatedMovie.Price)
                .SetProperty(m => m.ReleaseDate, updatedMovie.ReleaseDate)
                .SetProperty(m => m.GenreId, updatedMovie.GenreId)
                );
                return Results.NoContent();
            });
            //DELETE/Movies/{id}
            group.MapDelete("/{id}", async (MovieContext movieContext, int id) =>
            {
                if (!await movieContext.Movies.AnyAsync(m => m.Id == id))
                {
                    return Results.NotFound();
                }
                await movieContext.Movies.Where(m => m.Id == id).ExecuteDeleteAsync();
                return Results.NoContent();
            });

            return group;

        }
    }
}
