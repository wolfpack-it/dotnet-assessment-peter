using FluentValidation;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database.Constants;

namespace Wolfpack.Business.Validation.Pack;

internal abstract class PackForModificationValidator<T> : AbstractValidator<T>
    where T : PackForModificationModel
{
    protected PackForModificationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Constants.MaxNameLength)
            .Must(BeUnique).WithMessage(model => $"The pack name must be unique. A pack with the name {model.Name} already exists.");
    }

    protected abstract bool BeUnique(T model, string name);
}