using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Data.Database.EntityConfiguration;

internal class WolfConfiguration : IEntityTypeConfiguration<Wolf>
{
    public void Configure(EntityTypeBuilder<Wolf> builder)
    {
        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Gender)
            .HasConversion<string>();

        builder.Property(x => x.Latitude).HasPrecision(Wolf.LatitudePrecision, Wolf.LatitudeScale);
        builder.Property(x => x.Longitude).HasPrecision(Wolf.LongitudePrecision, Wolf.LongitudeScale);
    }
}