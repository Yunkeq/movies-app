using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.Endpoints;
using System.Data.Common;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDb")));

//var options = new DbContextOptionsBuilder<MovieContext>().UseSqlServer(builder.Configuration.GetConnectionString("MovieDb")).Options;

//MovieContext movieContext = new MovieContext(options);


WebApplication app = builder.Build();
using (var scope = app.Services.CreateScope())
{
        var context = scope.ServiceProvider.GetRequiredService<MovieContext>();
        await context.Database.MigrateAsync();
}

app.MapGet("/", () => "Hello World!");
app.MapMoviesEndpoints();
app.MapGenresEndpoints();

app.Run();