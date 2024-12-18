using Movies.DTO;

namespace Movies.Services.Interfaces;

public interface IMovieService
{
    public Task<MovieResponseDTO> GetMovies(string name, int page=1);
    
    public Task<SearchByIdResult> GetMovieById(int id);
    
}