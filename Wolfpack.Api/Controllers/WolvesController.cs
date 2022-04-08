using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;
using Wolfpack.Business.Models.Wolf;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Wolfpack.Api.Controllers;

/// <summary>
/// Controller for managing wolves.
/// </summary>
[ApiController]
[Route("[controller]")]
public class WolvesController : WolfpackController
{
    private readonly IWolfService _wolfService;


    /// <summary>
    /// Initializes a new instance of <see cref="WolvesController"/>.
    /// </summary>
    /// <param name="wolfService">The <see cref="IWolfService"/>.</param>
    public WolvesController(IWolfService wolfService)
    {
        _wolfService = wolfService;
    }

    /// <summary>
    /// Gets a list of all the wolves.
    /// </summary>
    /// <returns>A list of all the wolves.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WolfModel>))]
    public async Task<IActionResult> Get()
    {
        return GetActionResult(await _wolfService.GetAll());
    }

    /// <summary>
    /// Gets a specific wolf by id.
    /// </summary>
    /// <param name="id">The id of the wolf to retrieve.</param>
    /// <returns>The retrieved wolf.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WolfWithPacksModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([Required] Guid id)
    {
        return GetActionResult(await _wolfService.GetById(id));
    }

    /// <summary>
    /// Creates a new wolf.
    /// </summary>
    /// <remarks>
    /// The name of the wolf must be unique.
    /// </remarks>
    /// <param name="forCreationModel">The model with which to create the wolf.</param>
    /// <returns>The newly created wolf.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WolfModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(WolfForCreationModel forCreationModel)
    {
        var result = await _wolfService.Create(forCreationModel);

        if (result.ServiceResultCode != ServiceResultCode.Created)
        {
            return GetActionResult(result);
        }

        return CreatedAtAction(nameof(Get), new { id = result.TargetObject!.Id }, result.TargetObject);
    }

    /// <summary>
    /// Updates an existing wolf.
    /// </summary>
    /// <remarks>
    /// The name of the wolf must be unique.
    /// </remarks>
    /// <param name="id">The id of the wolf to update.</param>
    /// <param name="forUpdate">The model with which to update the wolf.</param>
    /// <returns>The newly updated wolf.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([Required] Guid id, [FromBody][Required] WolfForUpdateModel forUpdate)
    {
        return GetActionResult(await _wolfService.Update(id, forUpdate));
    }

    /// <summary>
    /// Deletes a wolf.
    /// </summary>
    /// <param name="id">The id of the wolf to delete.</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        return GetActionResult(await _wolfService.Delete(id));
    }
}