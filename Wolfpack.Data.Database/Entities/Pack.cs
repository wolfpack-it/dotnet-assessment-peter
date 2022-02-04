using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wolfpack.Data.Database.Entities;

public class Pack
{
    public const int MaxNameLength = 64;

    public const int LatitudePrecision = 10;
    public const int LatitudeScale = 7;
    public const int LongitudePrecision = 10;
    public const int LongitudeScale = 7;

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
}