using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MusicModel.Models;


public partial class MuscContext : IdentityDbContext<MusicUser>
{
    public MuscContext()
    {
    }

    public MuscContext(DbContextOptions<MuscContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.development.json", optional: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Song>(entity =>
        {
            entity.HasOne(d => d.Artist)
                .WithMany(p => p.Songs)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Songs_Artist");
        });

        modelBuilder.Entity<Artist>(entity =>
        {

            entity.Property(e => e.Genre).IsFixedLength();
        });


    OnModelCreatingPartial(modelBuilder);
    }

    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
