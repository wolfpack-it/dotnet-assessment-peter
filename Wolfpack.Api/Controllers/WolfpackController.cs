using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Models;

namespace Wolfpack.Api.Controllers;

/// <summary>
/// Base Wolfpack controller containing shared functionality.
/// </summary>
public abstract class WolfpackController : ControllerBase
{
    /// <summary>
    /// Creates the appropriate <see cref="IActionResult"/> from a <see cref="IServiceResponse{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IServiceResponse{T}"/></typeparam>
    /// <param name="serviceResponse">The service response to create the <see cref="IActionResult"/> for.</param>
    /// <returns>The appropriate <see cref="IActionResult"/>.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="ISimpleServiceResponse"/> cannot be translated to an <see cref="IActionResult"/>.</exception>
    protected IActionResult GetActionResult<T>(IServiceResponse<T> serviceResponse)
    {
        return serviceResponse.ServiceResultCode switch
        {
            ServiceResultCode.Ok => Ok(serviceResponse.TargetObject),
            ServiceResultCode.Created => throw new InvalidOperationException(
                $"{ServiceResultCode.Created} is not supported. Please create the ActionResult manually."),
            ServiceResultCode.NotFound => NotFound(),
            ServiceResultCode.ValidationError => BadRequest(serviceResponse.ValidationResult),
            ServiceResultCode.Conflict => Conflict(),
            _ => throw new InvalidOperationException("Server Error: Unexpected service response")
        };
    }

    /// <summary>
    /// Creates the appropriate <see cref="IActionResult"/> from a <see cref="ISimpleServiceResponse"/>.
    /// </summary>
    /// <param name="serviceResponse">The service response to create the <see cref="IActionResult"/> for.</param>
    /// <returns>The appropriate <see cref="IActionResult"/>.</returns>
    /// <exception cref="InvalidOperationException">The <see cref="ISimpleServiceResponse"/> cannot be translated to an <see cref="IActionResult"/>.</exception>
    protected IActionResult GetActionResult(ISimpleServiceResponse serviceResponse)
    {
        return serviceResponse.ServiceResultCode switch
        {
            ServiceResultCode.Ok => NoContent(),
            ServiceResultCode.Created => NoContent(),
            ServiceResultCode.NotFound => NotFound(),
            ServiceResultCode.ValidationError => BadRequest(serviceResponse.ValidationResult),
            ServiceResultCode.Conflict => Conflict(),
            _ => throw new InvalidOperationException("Server Error: Unexpected service response")
        };
    }
}