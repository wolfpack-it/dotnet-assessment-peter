using FluentValidation;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Data.Database.Entities;

namespace Wolfpack.Business.Validation
{
    internal class PackForModificationValidator<T> : AbstractValidator<T>
        where T : PackForModificationModel
    {
        private const int MinimumLatitude = -90;
        private const int MaximumLatitude = +90;

        private const int MinimumLongitude = -180;
        private const int MaximumLongitude = +180;

        public PackForModificationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(Pack.MaxNameLength);

            RuleFor(x => x.Latitude)
                .InclusiveBetween(MinimumLatitude, MaximumLatitude)
                .ScalePrecision(Pack.LatitudeScale, Pack.LatitudePrecision);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(MinimumLongitude, MaximumLongitude)
                .ScalePrecision(Pack.LongitudeScale, Pack.LongitudePrecision);
        }
    }

    internal class PackForUpdateModelValidator : PackForModificationValidator<PackForUpdateModel>
    {

    }

    internal class PackForCreationValidator : PackForModificationValidator<PackForCreationModel>
    {

    }
}
