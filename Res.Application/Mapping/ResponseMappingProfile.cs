using AutoMapper;
using Res.Common.Helpers;
using Res.Domain.Dto.Request.Create;
using Res.Domain.Dto.Response;
using Res.Domain.Entities;
using Res.Domain.Enumerations;

namespace Res.Application.Mapping;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<Category, CategoryResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

        CreateMap<BranchStore, BranchStoreResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted))
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted)))
        .AfterMap(
            (src, dest) =>
            {
                var Manager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 4) ?? new BranchStoreEmployee();
                dest.ManagerId = Manager.EmployeeId;
                var employeeManagerName = src.Employee.FirstOrDefault(x => x.Id == Manager.EmployeeId) ?? new Employee();
                dest.ManagerName = employeeManagerName.FullName;

                var CoManager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 43) ?? new BranchStoreEmployee();
                dest.CoManagerId = CoManager.EmployeeId;
                var employeeCoManagerName = src.Employee.FirstOrDefault(x => x.Id == CoManager.EmployeeId) ?? new Employee();
                dest.CoManagerName = employeeCoManagerName.FullName;

                var branchStoreAddress = src.Address.FirstOrDefault() ?? new Address();

                dest.Address1 = branchStoreAddress.Address1;
                dest.Address2 = branchStoreAddress.Address2;
                dest.Street = branchStoreAddress.Street;
                dest.ExternalNumber = branchStoreAddress.ExternalNumber;
                dest.InternalNumber = branchStoreAddress.InternalNumber;
                dest.City = branchStoreAddress.City;
                dest.ZipCode = branchStoreAddress.ZipCode;
            }
        );


    }
}