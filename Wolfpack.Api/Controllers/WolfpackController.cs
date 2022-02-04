using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Models;

namespace Wolfpack.Api.Controllers;

public abstract class WolfpackController : ControllerBase
{
    protected IActionResult GetActionResult<TK>(IServiceResponse<TK> serviceResponse)
    {
        return serviceResponse.ServiceResultCode switch
        {
            ServiceResultCode.Ok => Ok(serviceResponse.TargetObject),
            ServiceResultCode.Created => throw new InvalidOperationException($"{ServiceResultCode.Created} is not supported. Please create the ActionResult manually."),
            ServiceResultCode.NotFound => NotFound(),
            ServiceResultCode.ValidationError => BadRequest(serviceResponse.ValidationResult),
            ServiceResultCode.Conflict => Conflict(),
            _ => throw new Exception("Server Error: Unexpected service response")
        };
    }

    protected IActionResult GetActionResult(ISimpleServiceResponse serviceResponse)
    {
        return serviceResponse.ServiceResultCode switch
        {
            ServiceResultCode.Ok => NoContent(),
            ServiceResultCode.Created => NoContent(),
            ServiceResultCode.NotFound => NotFound(),
            ServiceResultCode.ValidationError => BadRequest(serviceResponse.ValidationResult),
            ServiceResultCode.Conflict => Conflict(),
            _ => throw new Exception("Server Error: Unexpected service response")
        };
    }
}