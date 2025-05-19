using MoviesBlazor.Models;

namespace MoviesBlazor.Clients
{
    public class MoviesClient(HttpClient httpClient)
    {
        public async Task<List<Movie>> GetAllMoviesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Movie>>("movies") ?? throw new Exception("Could not find movies.");
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await httpClient.GetFromJsonAsync<Movie>($"movies/{id}") ?? throw new Exception("Could not find movie.");
        }

        public async Task AddMovieAsync(Movie movie)
        {
            await httpClient.PostAsJsonAsync<Movie>($"movies", movie);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            await httpClient.PutAsJsonAsync<Movie>($"movies/{movie.Id}", movie);
        }

        public async Task DeleteMovieAsync(int id)
        {
            await httpClient.DeleteAsync($"movies/{id}");
        }
    }
}
