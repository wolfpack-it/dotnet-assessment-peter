using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacksController : WolfpackController
    {
        private readonly IPackService _packService;

        public PacksController(IPackService packService)
        {
            _packService = packService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PackModel>))]
        public async Task<IActionResult> Get()
        {
            return GetActionResult(await _packService.GetAll());
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([Required] Guid id)
        {
            return GetActionResult(await _packService.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PackModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(PackForCreationModel forCreationModel)
        {
            var result = await _packService.Create(forCreationModel);

            if (result.ServiceResultCode != ServiceResultCode.Created)
            {
                return GetActionResult(result);
            }

            return Created(nameof(Get), result.TargetObject);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PackModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([Required] Guid id, [FromBody, Required] PackForUpdateModel forUpdate)
        {
            return GetActionResult(await _packService.Update(id, forUpdate));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([Required] Guid id)
        {
            return GetActionResult(await _packService.Delete(id));
        }
    }
}