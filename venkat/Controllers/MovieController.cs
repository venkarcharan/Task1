using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using venkat.Common.Models;
using venkat.service.Abstraction;

namespace venkat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET ALL MOVIES WITH PAGINATION
        [HttpGet]
        public async Task<IActionResult> GetMoviesAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var result =
                    await _movieService.GetMoviesAsync(
                        pageNumber,
                        pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET MOVIE BY GUID
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetMovieByGuidAsync(string guid)
        {
            try
            {
                var result =
                    await _movieService.GetMovieByGuidAsync(guid);

                if (result == null)
                {
                    return NotFound("Movie Not Found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // INSERT MOVIE
        [HttpPost]
        public async Task<IActionResult> InsertMovieAsync(Movie movie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _movieService.InsertMovieAsync(movie);

                return Ok("Inserted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE MOVIE
        [HttpPut]
        public async Task<IActionResult> UpdateMovieAsync(Movie movie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _movieService.UpdateMovieAsync(movie);

                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE MOVIE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);

                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}