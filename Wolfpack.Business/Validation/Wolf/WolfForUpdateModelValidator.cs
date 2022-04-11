using Wolfpack.Business.Models.Wolf;
using Wolfpack.Data.Database;

namespace Wolfpack.Business.Validation.Wolf;

internal sealed class WolfForUpdateModelValidator : WolfForModificationValidator<WolfForUpdateModel>
{
    private readonly WolfpackContext _context;
    private readonly Guid _id;

    public WolfForUpdateModelValidator(WolfpackContext context, Guid id)
    {
        _context = context;
        _id = id;
    }

    protected override bool BeUnique(WolfForUpdateModel model, string name)
    {
        var nameExistsOnAnotherEntity = _context.Wolves.Any(x => x.Name.ToLower() == name.ToLower() && x.Id != _id);

        return !nameExistsOnAnotherEntity;
    }
}