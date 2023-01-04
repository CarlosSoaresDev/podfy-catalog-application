using podfy_catalog_application.Models;
using podfy_catalog_application.Services;
using Microsoft.AspNetCore.Mvc;

namespace podfy_catalog_application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogQueryController : ControllerBase
    {
        private readonly ICatalogQueryService _catalogService;
        public CatalogQueryController(ICatalogQueryService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _catalogService.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            var result = await _catalogService.GetAsync(id);

            return Ok(result);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetAllWithFilterAsync(string? search, int? skip, int? take)
        {
            var result = await _catalogService.GetAllWithFilter(new CatalogFilterRequestDto(search, skip, take));

            return Ok(result);
        }
    }
}