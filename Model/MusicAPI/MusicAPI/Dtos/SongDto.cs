using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MusicAPI.Dtos;

public class SongDto
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = string.Empty;
    public int ArtistId { get; set; }
    public string ArtistName { get; set; } = string.Empty;

}