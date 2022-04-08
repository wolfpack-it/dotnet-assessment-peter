using Microsoft.EntityFrameworkCore;
using Wolfpack.Data.Database.Entities;
using Wolfpack.Data.Database.EntityConfiguration;

namespace Wolfpack.Data.Database;

public class WolfpackContext : DbContext
{
    public WolfpackContext(DbContextOptions<WolfpackContext> options)
        : base(options)
    {
    }

    public DbSet<Pack> Packs { get; set; } = null!;

    public DbSet<Wolf> Wolves { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PackConfiguration());
        modelBuilder.ApplyConfiguration(new WolfConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}