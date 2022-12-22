using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicModel.Models;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly MuscContext _context;

        public SongsController(MuscContext context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs() => await _context.Songs.Take(100).ToListAsync();

        // GET: api/Songs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            Song? Song = await _context.Songs.FindAsync(id);

            return Song == null ? NotFound() : Song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutSong(int id, Song Song)
        {
            if (id != Song.Id)
            {
                return BadRequest();
            }

            _context.Entry(Song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song Song)
        {
            _context.Songs.Add(Song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = Song.Id }, Song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            Song? Song = await _context.Songs.FindAsync(id);
            if (Song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(Song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id) => _context.Songs.Any(e => e.Id == id);
    }
}
