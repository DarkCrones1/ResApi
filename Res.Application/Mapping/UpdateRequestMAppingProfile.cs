using AutoMapper;
using Res.Common.Helpers;
using Res.Domain.Dto.Request.Create;
using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class UpdateRequestMAppingProfile : Profile
{
    public UpdateRequestMAppingProfile()
    {
        CreateMap<CategoryUpdateRequestDto, Category>();

        CreateMap<BaseCatalogUpdateRequestDto, Rol>();

        CreateMap<BaseCatalogUpdateRequestDto, Job>();
    }
}