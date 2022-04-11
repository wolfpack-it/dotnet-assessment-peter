using Wolfpack.Business.Models.Wolf;

namespace Wolfpack.Business.Models.Pack;

public class PackWithWolvesModel : PackModel
{
    public ICollection<WolfModel> Wolves { get; set; } = new List<WolfModel>();
}