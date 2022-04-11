using Wolfpack.Business.Models.Wolf;
using Wolfpack.Data.Database;

namespace Wolfpack.Business.Validation.Wolf;

internal sealed class WolfForCreationValidator : WolfForModificationValidator<WolfForCreationModel>
{
    private readonly WolfpackContext _context;

    public WolfForCreationValidator(WolfpackContext context)
    {
        _context = context;
    }

    protected override bool BeUnique(WolfForCreationModel model, string name)
    {
        var nameExists = _context.Wolves.Any(x => x.Name.ToLower() == name.ToLower());

        return !nameExists;
    }
}