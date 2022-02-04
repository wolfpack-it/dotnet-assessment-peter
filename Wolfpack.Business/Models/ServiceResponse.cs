using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Wolfpack.Business.Models
{
    internal class ServiceResponse<T> : IServiceResponse<T>
    {
        private ServiceResponse(ValidationResult validationResult)
        {
            ServiceResultCode = ServiceResultCode.ValidationError;
            ValidationResult = validationResult;
        }

        private ServiceResponse(ServiceResultCode serviceResultCode)
        {
            ServiceResultCode = serviceResultCode;
            ValidationResult = null;
        }

        private ServiceResponse(ServiceResultCode serviceResultCode, T targetObject)
        {
            ServiceResultCode = serviceResultCode;
            ValidationResult = null;
            TargetObject = targetObject;
        }

        public ServiceResultCode ServiceResultCode { get; set; }

        public ValidationResult? ValidationResult { get; set; }

        public T? TargetObject { get; set; }

        public static ServiceResponse<T> Fail(ValidationResult validationResult) => new(validationResult);
        public static ServiceResponse<T> Ok(T value) => new(ServiceResultCode.Ok, value);
        public static ServiceResponse<T> Ok(ServiceResultCode resultCode) => new(resultCode);
        public static ServiceResponse<T> Ok(ServiceResultCode resultCode, T value) => new(resultCode, value);
    }

    internal static class ServiceResponse
    {
        public static ServiceResponse<T> Fail<T>(ValidationResult validationResult) => ServiceResponse<T>.Fail(validationResult);
        public static ServiceResponse<T> Ok<T>(T value) => ServiceResponse<T>.Ok(value);
        public static ServiceResponse<T> Ok<T>(ServiceResultCode resultCode) => ServiceResponse<T>.Ok(resultCode);
        public static ServiceResponse<T> Ok<T>(ServiceResultCode resultCode, T value) => ServiceResponse<T>.Ok(resultCode, value);
    }
}
