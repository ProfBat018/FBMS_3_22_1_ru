using System.Text.Json;
using Movies.Contexts;
using Movies.DTO;
using Movies.Services.Interfaces;
using RestSharp;

namespace Movies.Services.Classes;

public class MovieService : IMovieService
{
    private readonly MovieContext _movieContext;
    private readonly IConfiguration _configuration;
    
    public MovieService(MovieContext movieContext, IConfiguration configuration)
    {
        _movieContext = movieContext;
        _configuration = configuration;
    }
    
    public async Task<MovieResponseDTO> GetMovies(string name, int page=1)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("Name cannot be null or empty");
        }

        if (page <= 0)
        {
            page = 1;
        }
        
        var options = new RestClientOptions($"https://api.themoviedb.org/3/search/movie?query=batman&include_adult=false&language=en-US&page={page}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_configuration["TmdbApi:Key"]}");
       
        var response = await client.GetAsync(request);

        if (!response.IsSuccessful)
        {
            throw new Exception("Failed to get movies");
        }
        
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        
        await writer.WriteAsync(response.Content);
        await writer.FlushAsync();
        memoryStream.Position = 0;
        
        return await JsonSerializer.DeserializeAsync<MovieResponseDTO>(memoryStream) ?? throw new Exception("Failed to get movies");
    }

    public async Task<SearchByIdResult> GetMovieById(int id)
    {
        var externalIds = await GetExternalIdsAsync(id);
        
        // Проверям через рефлексию, все null поля нашего объекта и берем те кто не null с помощью nameof и добавляем их в словарь 
        // и возвращаем его
        
        Dictionary<string, string> externalIdsDictionary = new();

        var filteredResult = externalIds.GetType().GetProperties()
            .Where(f => f.GetValue(externalIds) != null & f.Name != "id")
            .Select(x => x.Name).ToList();
        
        foreach (var item in filteredResult)
        {
            externalIdsDictionary.Add(item, externalIds.GetType().GetProperty(item)?.GetValue(externalIds).ToString());
        }
        

        foreach (var item in externalIdsDictionary)
        {
            
            var options = new RestClientOptions($"https://api.themoviedb.org/3/find/tt0096895?external_source={item.Key}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", $"Bearer {_configuration["TmdbApi:Key"]}");
            var response = await client.GetAsync(request);
            if (response.IsSuccessful)
            {
                using var memoryStream = new MemoryStream();
                using var writer = new StreamWriter(memoryStream);
                await writer.WriteAsync(response.Content);
                await writer.FlushAsync();
                memoryStream.Position = 0;
                return await JsonSerializer.DeserializeAsync<SearchByIdResult>(memoryStream) ?? throw new Exception("Failed to get movie by id");
                break;
            }
        }
        
        throw new Exception("Failed to get movie by id");
    }

    private async Task<ExternalIdResponseDTO> GetExternalIdsAsync(int id)
    {
        
        var options = new RestClientOptions("https://api.themoviedb.org/3/movie/268/external_ids");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("Authorization", $"Bearer {_configuration["TmdbApi:Key"]}");
        var response = await client.GetAsync(request);

        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        
        await writer.WriteAsync(response.Content);
        await writer.FlushAsync();
        memoryStream.Position = 0;
        
        return await JsonSerializer.DeserializeAsync<ExternalIdResponseDTO>(memoryStream) ?? throw new Exception("Failed to get external ids");
    }
}
