using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Models;
using Movies.Services.Interfaces;

namespace Movies.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase 
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpGet("{name}/{page=1}")]
    public async Task<IActionResult> GetMovies(string name, int page=1)
    {
        var res = await _movieService.GetMovies(name, page);

        if (res == null)
            throw new Exception("Failed to get movies");

        return Ok(ResponseModel<MovieResponseDTO>.SuccessResponse(res, "Movies retrieved successfully"));
    }

    [Authorize("AppUser")]
    [HttpGet("Save/{id}")]
    public async Task<IActionResult> SaveMovieToCollection(int id)
    {
        var res = await _movieService.GetMovieById(id);

        if (res == null)
            throw new Exception("Failed to get movie");

        return Ok(ResponseModel<SearchByIdResult>.SuccessResponse(res, "Movie retrieved successfully"));
    }
}