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

        CreateMap<BranchStoreCreateRequestDto, BranchStore>()
        .ForMember(
            dest => dest.Employee,
            opt => opt.MapFrom(src => src.Employees)
        )
        .ForMember(
            dest => dest.BoxCash,
            opt => opt.MapFrom(src => src.BoxCashes)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;
                // var createdUser = context.Items["CreatedUser"] as string;
                dest.CreatedBy = "admin";

                var locationAddress = new Address
                {
                    Address1 = src.Address1,
                    Address2 = src.Address2,
                    Street = src.Street,
                    ExternalNumber = src.ExternalNumber,
                    InternalNumber = src.InternalNumber,
                    ZipCode = src.ZipCode,
                    City = src.City!
                };
                dest.Address.Add(locationAddress);
            }
        );

        CreateMap<BoxCashCreateRequestDto, BoxCash>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        );

        CreateMap<EmployeeBranchStoreCreateRequestDto, Employee>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted))
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now))
        .AfterMap(
            (src, dest, context) =>
            {
                var employeeAddress = new Address
                {
                    Address1 = src.Address1,
                    Address2 = src.Address2,
                    Street = src.Street,
                    ExternalNumber = src.ExternalNumber,
                    InternalNumber = src.InternalNumber,
                    ZipCode = src.ZipCode,
                    City = src.City!
                };
                dest.Address.Add(employeeAddress);

                // var createdUser = context.Items["CreatedUser"] as string;
                dest.CreatedBy = "admin";
            }
        );
    }
}