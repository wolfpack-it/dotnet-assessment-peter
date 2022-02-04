using FluentValidation.Results;

namespace Wolfpack.Business.Models;

public interface ISimpleServiceResponse
{
    public ServiceResultCode ServiceResultCode { get; set; }

    public ValidationResult? ValidationResult { get; set; }
}