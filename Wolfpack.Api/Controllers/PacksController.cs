using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Wolfpack.Api.Controllers;

/// <summary>
/// Controller for managing packs.
/// </summary>
[ApiController]
[Route("[controller]")]
public sealed class PacksController : WolfpackController
{
    private readonly IPackService _packService;

    /// <summary>
    /// Initializes a new instance of <see cref="PacksController"/>.
    /// </summary>
    /// <param name="packService">The <see cref="IPackService"/>.</param>
    public PacksController(IPackService packService)
    {
        _packService = packService;
    }

    /// <summary>
    /// Gets a list of all the packs.
    /// </summary>
    /// <returns>A list of all the packs.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PackModel>))]
    public async Task<IActionResult> Get()
    {
        return GetActionResult(await _packService.GetAll());
    }

    /// <summary>
    /// Gets a specific pack by id.
    /// </summary>
    /// <param name="id">The id of the pack to retrieve.</param>
    /// <returns>The retrieved pack.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([Required] Guid id)
    {
        return GetActionResult(await _packService.GetById(id));
    }

    /// <summary>
    /// Creates a new pack.
    /// </summary>
    /// <remarks>
    /// The name of the pack must be unique.
    /// </remarks>
    /// <param name="forCreationModel">The model with which to create the pack.</param>
    /// <returns>The newly created pack.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PackModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(PackForCreationModel forCreationModel)
    {
        var result = await _packService.Create(forCreationModel);

        if (result.ServiceResultCode != ServiceResultCode.Created)
        {
            return GetActionResult(result);
        }

        return CreatedAtAction(nameof(Get), new { id = result.TargetObject!.Id }, result.TargetObject);
    }

    /// <summary>
    /// Updates an existing pack.
    /// </summary>
    /// <remarks>
    /// The name of the pack must be unique.
    /// </remarks>
    /// <param name="id">The id of the pack to update.</param>
    /// <param name="forUpdate">The model with which to update the pack.</param>
    /// <returns>The newly updated pack.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([Required] Guid id, [FromBody][Required] PackForUpdateModel forUpdate)
    {
        return GetActionResult(await _packService.Update(id, forUpdate));
    }

    /// <summary>
    /// Deletes a pack.
    /// </summary>
    /// <param name="id">The id of the pack to delete.</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([Required] Guid id)
    {
        return GetActionResult(await _packService.Delete(id));
    }

    /// <summary>
    /// Deletes a wolf from a pack.
    /// </summary>
    /// <param name="id">The id of the pack to delete.</param>
    /// <returns></returns>
    [HttpDelete("{id:guid}/wolves/{wolf_id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWolfFromPack([Required] Guid id, [Required] Guid wolf_id)
    {
        return GetActionResult(await _packService.DeleteWolfFromPack(id, wolf_id));
    }

    /// <summary>
    /// Add a wolf to a pack.
    /// </summary>
    /// <param name="id">The id of the pack to add.</param>
    /// <param name="model">The model containing the wolf that needs to be added to the pack</param>
    /// <returns></returns>
    [HttpPut("{id:guid}/wolves/{wolf_id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackWithWolvesModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddWolfToPack([Required] Guid id, [FromBody][Required]AddWolfToPackModel model)
    {
        return GetActionResult(await _packService.AddWolfToPack(id, model.Id)); 
    }
}