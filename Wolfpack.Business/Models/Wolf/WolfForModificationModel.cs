using Wolfpack.Data.Database.Enums;

namespace Wolfpack.Business.Models.Wolf;

public class WolfForModificationModel
{
    public string Name { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public Gender Gender { get; set; }
}