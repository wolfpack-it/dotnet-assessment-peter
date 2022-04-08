using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database;

namespace Wolfpack.Business.Validation.Pack;

internal abstract class PackForAddWolfToPackValidator<T> : AbstractValidator<T>
    where T : AddWolfToPackModel
{
    protected PackForAddWolfToPackValidator()
    {
        RuleFor(x => x.Id)
            .Must(BeUnique)
            .WithMessage(model => $"A wolf with id {model.Id} already exists in the pack.");
    }

    protected abstract bool BeUnique(T model, Guid wolfId);
}

internal sealed class PackForAddWolfToPackModelValidator : PackForAddWolfToPackValidator<AddWolfToPackModel>
{
    private readonly WolfpackContext _context;
    private readonly Guid _packId;

    public PackForAddWolfToPackModelValidator(WolfpackContext context, Guid packId)
    {
        _context = context;
        _packId = packId;
    }

    protected override bool BeUnique(AddWolfToPackModel model, Guid wolfId)
    {
        var entity = _context.Packs.Include(x => x.Wolves)
            .Where(x => x.Wolves.Any(w => w.Id == wolfId))
            .FirstOrDefault(x => x.Id == _packId);

        return entity is null;
    }
}