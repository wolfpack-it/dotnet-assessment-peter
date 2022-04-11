using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Business.Models.Wolf;

public class WolfWithPacksModel : WolfModel
{
    public ICollection<PackModel> Packs { get; set; } = new List<PackModel>();
}