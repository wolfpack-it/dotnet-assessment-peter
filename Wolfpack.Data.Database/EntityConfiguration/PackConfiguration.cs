using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Data.Database.EntityConfiguration;

internal class PackConfiguration : IEntityTypeConfiguration<Pack>
{
    public void Configure(EntityTypeBuilder<Pack> builder)
    {
        builder.Property(x => x.Latitude).HasPrecision(Pack.LatitudePrecision, Pack.LatitudeScale);
        builder.Property(x => x.Longitude).HasPrecision(Pack.LongitudePrecision, Pack.LongitudeScale);
    }
}