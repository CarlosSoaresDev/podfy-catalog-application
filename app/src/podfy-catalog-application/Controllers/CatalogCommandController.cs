using podfy_catalog_application.Models;
using podfy_catalog_application.Services;
using Microsoft.AspNetCore.Mvc;

namespace podfy_catalog_application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogCommandController : ControllerBase
    {
        private readonly ICatalogCommandService _catalogCommandService;
        public CatalogCommandController(ICatalogCommandService catalogCommandService)
        {
            _catalogCommandService = catalogCommandService;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, CatalogRequestDto catalogRequestDto)
        {
            await _catalogCommandService.UpdateAsync(id, catalogRequestDto);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CatalogRequestDto catalogRequestDto)
        {
            await _catalogCommandService.CreateAsync(catalogRequestDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            await _catalogCommandService.DeleteAsync(id);

            return NoContent();
        }
    }
}