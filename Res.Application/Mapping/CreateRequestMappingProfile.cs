using AutoMapper;
using Res.Common.Helpers;
using Res.Domain.Dto.Request.Create;
using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class CreateRequestMappingProfile : Profile
{
    public CreateRequestMappingProfile()
    {
        CreateMap<CategoryCreateRequestDto, Category>()
        // .ForMember(
        //     dest => dest.CreatedBy,
        //     opt => opt.MapFrom(src => "Admin")
        // )
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
                var createdUser = context.Items["CreatedUser"] as string;
                dest.CreatedBy = createdUser;
            }
        );
    }
}