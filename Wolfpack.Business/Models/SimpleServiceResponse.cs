using FluentValidation.Results;

namespace Wolfpack.Business.Models
{
    public class SimpleServiceResponse : ISimpleServiceResponse
    {
        private SimpleServiceResponse(ValidationResult validationResult)
        {
            ServiceResultCode = ServiceResultCode.ValidationError;
            ValidationResult = validationResult;
        }

        private SimpleServiceResponse(ServiceResultCode serviceResultCode)
        {
            ServiceResultCode = serviceResultCode;
            ValidationResult = null;
        }

        public ServiceResultCode ServiceResultCode { get; set; }
        public ValidationResult? ValidationResult { get; set; }

        public static SimpleServiceResponse Fail(ValidationResult validationResult) => new(validationResult);
        public static SimpleServiceResponse Fail(ServiceResultCode serviceResultCode) => new(serviceResultCode);

        public static SimpleServiceResponse Ok = new(ServiceResultCode.Ok);
    }
}
