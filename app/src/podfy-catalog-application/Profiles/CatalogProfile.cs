using AutoMapper;
using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;
using System.Diagnostics.CodeAnalysis;

namespace podfy_catalog_application.Profiles
{
    [ExcludeFromCodeCoverage]
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<CatalogRequestDto, Catalog>()
                .ForMember(dist => dist.CreateAt, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CatalogResponseDto, Catalog>().ReverseMap();
        }
    }
}