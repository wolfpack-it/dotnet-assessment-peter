using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database;

namespace Wolfpack.Business.Validation;

internal sealed class PackForUpdateModelValidator : PackForModificationValidator<PackForUpdateModel>
{
    private readonly WolfpackContext _context;
    private readonly Guid _id;

    public PackForUpdateModelValidator(WolfpackContext context, Guid id)
    {
        _context = context;
        _id = id;
    }

    protected override bool BeUnique(PackForUpdateModel model, string name)
    {
        var nameExistsOnAnotherEntity = _context.Packs.Any(x => x.Name.ToLower() == name.ToLower() && x.Id != _id);

        return !nameExistsOnAnotherEntity;
    }
}