using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Business.Validation
{
    internal class PackValidator : AbstractValidator<PackForCreationModel>
    {
    }
}
