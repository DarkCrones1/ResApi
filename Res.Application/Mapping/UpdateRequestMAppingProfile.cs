using AutoMapper;
using Res.Common.Helpers;
using Res.Domain.Dto.Request.Update;
using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class UpdateRequestMappingProfile : Profile
{
    public UpdateRequestMappingProfile()
    {
        CreateMap<BoxCashUpdateRequestDto, BoxCash>();

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

        CreateMap<DrinkUpdateRequestDto, Drink>();

        CreateMap<FoodUpdateRequestDto, Food>();
    }
}