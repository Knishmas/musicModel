//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using MusicAPI.Dtos;
using MusicModel.Models;

    

namespace MusicAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase
{
    private readonly MuscContext _context;

    public ArtistsController(MuscContext context)
    {
        _context = context;
    }

    // GET: api/Artists
    //[Authorize]
    [HttpGet]
    //Getting some errors with just using "Artist" below so I changed it to "Name" for now, may need to come back
    //If I can change "Artist" to "name" I'll be fine 
    public async Task<ActionResult<IEnumerable<Artist>>> GetArtists() => await _context.Artists.OrderBy(c => c.ArtistName).ToListAsync();

    // GET: api/Artists/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetArtist(int id)
    {
        var ArtistDTO = await _context.Artists
            .Select(c => new
            {
                c.Id,
                //Also changing Name to name here for test sake
                c.ArtistName,
                c.Genre
            })
            .SingleOrDefaultAsync(c => c.Id == id);


        return ArtistDTO == null ? NotFound() : Ok(ArtistDTO);
    }



    [HttpGet("ArtistSongs/{id:int}")]
    public async Task<ActionResult> GetArtistSongs(int id)
    {
        var ArtistDTO = await _context.Artists
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.ArtistName,
                c.Songs
            }).SingleOrDefaultAsync();

        return ArtistDTO == null ? NotFound() : Ok(ArtistDTO);
    }

    [HttpGet("Songs/{id:int}")]
    public async Task<ActionResult<SongDto>> GetSongs(int id)
    {
        List<SongDto> songDto = await _context.Songs
            .Where(c => c.ArtistId == id)
            .Select(c => new SongDto
            {
                Id = c.Id,
                //Originally should be name, but in database, titled " Song" causing some 
                //errors, temporary fi to check will be channging to  Artist
                Name = c.Name,
                ArtistId = c.ArtistId,
                ArtistName = c.Artist.ArtistName
            }).ToListAsync();
        
        return Ok(songDto);
    }

    // PUT: api/Artists/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutArtist(int id, Artist Artist)
    {
        if (id != Artist.Id)
        {
            return BadRequest();
        }

        _context.Entry(Artist).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ArtistExists(id))
            {
                return NotFound();
            }

            throw;
        }
        return NoContent();
    }

    // POST: api/Artists
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Artist>> PostArtist(Artist Artist)
    {
        _context.Artists.Add(Artist);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetArtist", new { id = Artist.Id }, Artist);
    }

    // DELETE: api/Artists/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteArtist(int id)
    {
        Artist? Artist = await _context.Artists.FindAsync(id);
        if (Artist == null)
        {
            return NotFound();
        }

        _context.Artists.Remove(Artist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ArtistExists(int id) => _context.Artists.Any(e => e.Id == id);
}