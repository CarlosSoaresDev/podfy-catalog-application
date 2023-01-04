using AutoMapper;
using podfy_catalog_application.Cache;
using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;
using podfy_catalog_application.Repository;
using System.Text.Json;

namespace podfy_catalog_application.Services;

public class CatalogQueryService : ICatalogQueryService
{
    private readonly IMapper _mapper;
    private readonly IRedisCache _redisCache;
    private readonly ILogger<CatalogQueryService> _logger;
    private readonly ICatalogQueryRepository _catalogQueryRepository;

    public CatalogQueryService(IMapper mapper
        , ILogger<CatalogQueryService> logger
        , ICatalogQueryRepository catalogQueryRepository
        , IRedisCache redisCache)
    {
        _mapper = mapper;
        _logger = logger;   
        _catalogQueryRepository = catalogQueryRepository;
        _redisCache = redisCache;
    }

    public async Task<IEnumerable<CatalogResponseDto>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("[CatalogQueryService] => [GetAllAsync] => Realizando get obter todos");
            var cache = _redisCache.StringGet("key_get_all");

            if (string.IsNullOrEmpty(cache))
            {
                _logger.LogInformation($"[CatalogQueryService] => [GetAllAsync] => Entrando no setar cache");

                cache = JsonSerializer.Serialize(await _catalogQueryRepository.GetAllAsync());
                _redisCache.StringSet("key_get_all", cache);
            }

            var catalogs = JsonSerializer.Deserialize<IEnumerable<Catalog>>(cache, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return _mapper.Map<IEnumerable<CatalogResponseDto>>(catalogs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogQueryService] => [GetAllAsync] => Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<CatalogResponseDto>> GetAllWithFilter(CatalogFilterRequestDto catalogFilterRequest)
    {
        try
        {
            _logger.LogInformation("[CatalogQueryService] => [GetAllWithFilter] => Realizando get por filtro");
            var catalogs = await _catalogQueryRepository.GetAllWithFilter(catalogFilterRequest);

            return _mapper.Map<IEnumerable<CatalogResponseDto>>(catalogs);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogQueryService] => [GetAllWithFilter] => Erro: {ex.Message}");
            throw;
        }
    }

    public async Task<CatalogResponseDto> GetAsync(long id)
    {
        try
        {
            _logger.LogInformation($"[CatalogQueryService] => [GetAsync] => Realizando get por id: {id}");
            var catalog = await _catalogQueryRepository.GetAsync(id);
            return _mapper.Map<CatalogResponseDto>(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"[CatalogQueryService] => [GetAsync] => Erro: {ex.Message}");
            throw;
        }
    }
}
