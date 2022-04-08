using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wolfpack.Data.Database.Enums;

namespace Wolfpack.Data.Database.Entities;

public class Wolf
{
    public const int LatitudePrecision = 10;
    public const int LatitudeScale = 7;
    public const int MinimumLatitude = -90;
    public const int MaximumLatitude = +90;

    public const int LongitudePrecision = 10;
    public const int LongitudeScale = 7;
    public const int MinimumLongitude = -180;
    public const int MaximumLongitude = +180;

    public Wolf()
    {
        Packs = new HashSet<Pack>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string Name { get; set; } = null!;

    [Required]
    public decimal Latitude { get; set; }

    [Required]
    public decimal Longitude { get; set; }

    [Required]
    public Gender Gender { get; set; }

    public ICollection<Pack> Packs { get; set; }
}