using AutoMapper;
using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;
using podfy_catalog_application.Repository;

namespace podfy_catalog_application.Services;

public class CatalogCommandService : ICatalogCommandService
{
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogCommandService> _logger;
    private readonly ICatalogCommandoRepository _catalogCommandRepository;
    private readonly ICatalogQueryRepository _catalogQueryRepository;

    public CatalogCommandService(IMapper mapper, ILogger<CatalogCommandService> logger, ICatalogCommandoRepository catalogCommandRepository, ICatalogQueryRepository catalogQueryRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _catalogCommandRepository = catalogCommandRepository;
        _catalogQueryRepository = catalogQueryRepository;
    }

    public async Task UpdateAsync(long id, CatalogRequestDto catalogRequestDto)
    {
        try
        {
            _logger.LogInformation("[CatalogCommandService] => [UpdateAsync] => Realizando update");

            var catalogBase = await _catalogQueryRepository.GetAsync(id);
            var catalog = _mapper.Map(catalogRequestDto, catalogBase);

            await _catalogCommandRepository.UpdateAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogCommandService] => [UpdateAsync] => Erro: {ex.Message}");
            throw;
        }
    }

    public async Task CreateAsync(CatalogRequestDto catalogRequestDto)
    {
        try
        {
            _logger.LogInformation("[CatalogCommandService] => [CreateAsync] => Realizando create");
            var catalog = _mapper.Map<CatalogRequestDto, Catalog>(catalogRequestDto);

            await _catalogCommandRepository.CreateAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogCommandService] => [CreateAsync] => Erro: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteAsync(long id)
    {
        try
        {
            _logger.LogInformation($"[CatalogCommandService] => [DeleteAsync] => Realizando delete do id: {id} ");
            var catalog = await _catalogQueryRepository.GetAsync(id);

            await _catalogCommandRepository.DeleteAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogCommandService] => [DeleteAsync] => Erro: {ex.Message}");
            throw;
        }
    }
}
