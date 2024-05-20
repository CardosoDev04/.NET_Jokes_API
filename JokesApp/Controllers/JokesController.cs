using JokesApp.Filters;
using JokesApp.Models;
using JokesApp.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JokesApp.Controllers
{
    [ApiController]
    public class JokesController : Controller
    {

        private readonly JokesContext dbContext;

    public JokesController(JokesContext dbContext) // Inject JokesContext
    {
      this.dbContext = dbContext;
    }
        [HttpGet]
        [Route("/jokes")]
        [Authorize]
        public async Task<List<Joke>> GetJokes()
        {
            return await JokesRepository.GetJokeList(dbContext);
        }

        [HttpGet]
        [Route("/jokes/{id}")]
        [Joke_ValidateJokeIdFilter]
        public async Task<IActionResult> GetJokeById(int id, [FromQuery] string? author)
        {
            var joke = await JokesRepository.GetJokeById(id,dbContext);
            if (joke == null) return NotFound();
            return Ok(joke);
        }

        [HttpPost]
        [Route("/jokes")]
        public async Task<IActionResult> CreateJoke([FromBody] Joke joke)
        {
           var returnedJoke = await JokesRepository.AddJoke(joke,dbContext);
            return Ok(returnedJoke);
        }

        [HttpPut]
        [Route("/jokes/{id}")]
        [Joke_ValidateJokeIdFilter]
        public async Task<IActionResult> UpdateJoke(int id, [FromBody] Joke updatedJoke)
        {
            var toUpdate = await JokesRepository.GetJokeById(id,dbContext);
            if (toUpdate == null) return NotFound();
            toUpdate.Author = updatedJoke.Author;
            toUpdate.Text = updatedJoke.Text;
            var joke = await JokesRepository.UpdateJoke(toUpdate,dbContext);
            return Ok(joke);
        }

        [HttpDelete]
        [Route("/jokes/{id}")]
        [Joke_ValidateJokeIdFilter]
        public async Task<IActionResult> DeleteJoke(int id)
        {
            var joke = await JokesRepository.RemoveJokeById(id,dbContext);
            if (joke == null) return NotFound();
            return Ok(joke);
        }

    }
}
