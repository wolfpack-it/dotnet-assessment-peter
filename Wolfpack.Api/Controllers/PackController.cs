using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Wolfpack.Business.Interface;
using Wolfpack.Business.Models.Pack;

namespace Wolfpack.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackController : WolfpackController
    {
        private readonly IPackService _packService;

        public PackController(IPackService packService)
        {
            _packService = packService;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return GetActionResult(await _packService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PackForCreationModel forCreationModel)
        {
            return GetActionResult(await _packService.Create(forCreationModel));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update([FromQuery] Guid id, [FromBody, Required] PackForModificationModel forModification)
        {
            return GetActionResult(await _packService.Update(forModification));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return GetActionResult(await _packService.Delete(id));
        }
    }
}