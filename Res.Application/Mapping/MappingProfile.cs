using AutoMapper;

using Res.Domain.Dto.Request.Create;

using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseCatalogCreateRequestDto, Rol>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                // var createdUser = context.Items["CreatedUser"] as string;
                // dest.CreatedBy = createdUser;
            }
        );

        CreateMap<BaseCatalogCreateRequestDto, Job>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .AfterMap(
            (src, dest, context) => 
            {
                
            }
        );
    }
}