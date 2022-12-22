using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicModel.Models;


public partial class Song
{
    [Key]
    public int Id { get; set; }

    [Column("Song")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = string.Empty;

    public int Year { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ArtistName { get; set; } = null!;
    public int ArtistId { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Songs")]

    public virtual Artist Artist { get; set; } = null!;
}
