using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusicModel.Models;

[Table("Artist")]
public partial class Artist
{
    [Key]
    public int Id { get; set; }

    [Column("Artist")]
    [StringLength(50)]
    [Unicode(false)]
    public string ArtistName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Genre { get; set; } = null!;

    [InverseProperty("Artist")]
    public virtual ICollection<Song> Songs { get; } = new List<Song>();
}
