using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LibraryAPI.Entities;
using LibraryAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Controllers
{
    [Route("api/Library/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieRepositroy repositriy;
        private readonly ILogger<MovieController> logger;

        public MovieController(IMovieRepositroy repositriy, ILogger<MovieController> logger)
        {
            this.repositriy = repositriy;
            this.logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Movie>>>GetMovies()
        {
            var movies = await repositriy.GetMovies();

            return Ok(movies);
        }
        [HttpGet("{id:length(24)}",Name = "GetMovie")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Movie>> GetMovieById(string id)
        {
            var movie = await repositriy.GetMovieById(id);

            if (movie == null)
            {
                logger.LogError($"Movie with id:{id} not found");
                return NotFound();
            }
             return Ok(movie);

        }
        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Movie>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMoviesByCategory(string category)
        {
            var movies = await repositriy.GetMoviesByCategory(category);
            if (movies == null)
            {
                logger.LogError($"Movie with category:{category} not found");
                return NotFound();
            }
            return Ok(movies);

        }
        /// <summary>Create Movie</summary>
        [HttpPost]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            await repositriy.Create(movie);

            return CreatedAtRoute("GetMovie", new { id = movie.Id }, movie);
        }
        [HttpPut]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Movie movie)
        {
            return Ok(await repositriy.Update(movie));
        }
        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Movie), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            return Ok(await repositriy.Delete(id));
        }
    }
}
