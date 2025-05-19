using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;

namespace MoviesAPI.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/genres");

            group.MapGet("/", async (MovieContext movieContext) =>
            {
                var genres = await movieContext.Genres
                .AsNoTracking()
                .ToListAsync();
                return genres is null ? Results.NotFound("no genres found") : Results.Ok(genres);
            });

            return group;
        }
    }
}
