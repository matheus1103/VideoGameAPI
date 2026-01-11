using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAllVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<VideoGame>> InsertVideoGame(VideoGame newGame)
        {
            if (newGame == null)
                return BadRequest();    
            _context.VideoGames.Add(newGame);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVideoGameById), new {id = newGame.Id }, newGame);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoGame(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);
            if (game is null)
            {
                return NotFound();
            }
            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVideoGame(int id, VideoGame updatedGame)
        {
            if (id != updatedGame.Id)
            {
                return BadRequest();
            }
            _context.Entry(updatedGame).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.VideoGames.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
    }
}
