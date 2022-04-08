using FluentValidation;
using Wolfpack.Business.Models.Wolf;
using Wolfpack.Data.Database.Constants;

namespace Wolfpack.Business.Validation.Wolf;

internal abstract class WolfForModificationValidator<T> : AbstractValidator<T>
    where T : WolfForModificationModel
{
    protected WolfForModificationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Constants.MaxNameLength)
            .Must(BeUnique).WithMessage(model => $"The wolf name must be unique. A wolf with the name {model.Name} already exists.");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Invalid gender specified.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(Data.Database.Entities.Wolf.MinimumLatitude, Data.Database.Entities.Wolf.MaximumLatitude)
            .ScalePrecision(Data.Database.Entities.Wolf.LatitudeScale, Data.Database.Entities.Wolf.LatitudePrecision);

        RuleFor(x => x.Longitude)
            .InclusiveBetween(Data.Database.Entities.Wolf.MinimumLongitude, Data.Database.Entities.Wolf.MaximumLongitude)
            .ScalePrecision(Data.Database.Entities.Wolf.LongitudeScale, Data.Database.Entities.Wolf.LongitudePrecision);
    }

    protected abstract bool BeUnique(T model, string name);
}