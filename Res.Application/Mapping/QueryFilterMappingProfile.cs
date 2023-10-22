using AutoMapper;

using Res.Common.QueryFilters;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Application.Mapping;

public class QueryFilterMappingProfile : Profile
{
    public QueryFilterMappingProfile()
    {
        CreateMap<BaseCatalogQueryFilter, Category>();

        CreateMap<CategoryQueryFilter, Category>();
    }
}