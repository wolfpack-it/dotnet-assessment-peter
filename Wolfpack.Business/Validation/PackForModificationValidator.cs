using FluentValidation;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Validation;

internal abstract class PackForModificationValidator<T> : AbstractValidator<T>
    where T : PackForModificationModel
{
    protected PackForModificationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Pack.MaxNameLength)
            .Must(BeUnique).WithMessage(model => $"The pack name must be unique. A pack with the name {model.Name} already exists.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(Pack.MinimumLatitude, Pack.MaximumLatitude)
            .ScalePrecision(Pack.LatitudeScale, Pack.LatitudePrecision);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(Pack.MinimumLongitude, Pack.MaximumLongitude)
            .ScalePrecision(Pack.LongitudeScale, Pack.LongitudePrecision);
    }

    protected abstract bool BeUnique(T model, string name);
}