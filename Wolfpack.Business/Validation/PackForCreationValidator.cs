using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database;

namespace Wolfpack.Business.Validation;

internal sealed class PackForCreationValidator : PackForModificationValidator<PackForCreationModel>
{
    private readonly WolfpackContext _context;

    public PackForCreationValidator(WolfpackContext context)
    {
        _context = context;
    }

    protected override bool BeUnique(PackForCreationModel model, string name)
    {
        var nameExists = _context.Packs.Any(x => x.Name.ToLower() == name.ToLower());

        return !nameExists;
    }
}