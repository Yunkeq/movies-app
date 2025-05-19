using MoviesBlazor.Models;

namespace MoviesBlazor.Clients
{
    public class GenresClient(HttpClient httpClient)
    {
        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Genre>>("/genres") ?? throw new Exception("Could not find genres.");
        }
    }
}
