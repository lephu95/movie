using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using movie.Playloads.DataRequest;
using movie.Services.Interface;

namespace movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        public MoviesController(IMoviesService moviesService)
        {
            _moviesService = moviesService;
        }
        [HttpPost("Arrage")]
       public IActionResult namemovie(int limit)
        {
            return Ok(_moviesService.NameMovies(limit));
        }

    }
}
