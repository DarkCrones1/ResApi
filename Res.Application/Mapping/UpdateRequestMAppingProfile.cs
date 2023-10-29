using AutoMapper;
using Res.Common.Helpers;
using Res.Domain.Dto.Request.Create;
using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class UpdateRequestMappingProfile : Profile
{
    public UpdateRequestMappingProfile()
    {
        CreateMap<CategoryUpdateRequestDto, Category>();

        CreateMap<BaseCatalogUpdateRequestDto, Rol>();

        CreateMap<BaseCatalogUpdateRequestDto, Job>();

        CreateMap<BranchStoreUpdateRequestDto, BranchStore>()
        .AfterMap(
            (src, dest) =>
            {
                var branchStoreAddress = new Address
                {
                    Address1 = src.Address1,
                    Address2 = src.Address2,
                    Street = src.Street,
                    ExternalNumber = src.ExternalNumber,
                    InternalNumber = src.InternalNumber,
                    ZipCode = src.ZipCode,
                    City = src.City!
                };
                dest.Address.Add(branchStoreAddress);
            }
        );

        CreateMap<CustomerUpdateRequestDto, Customer>();

        CreateMap<CustomerUpdateRequestDto, CustomerAddress>()
        .AfterMap(
            (src, dest) =>
            {
                dest.Address = new Address
                {
                    Address1 = src.Address1,
                    Address2 = src.Address2,
                    Street = src.Street,
                    ExternalNumber = src.ExternalNumber,
                    InternalNumber = src.InternalNumber,
                    City = src.City ?? string.Empty,
                    ZipCode = src.ZipCode
                };
                dest.RegisterDate = DateTime.Now;
            }
        );
    }
}