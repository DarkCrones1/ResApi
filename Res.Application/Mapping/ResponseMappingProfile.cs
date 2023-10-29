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
        CreateMap<BoxCash, BoxCashResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        )
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

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
                var Manager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 2) ?? new BranchStoreEmployee();
                dest.ManagerId = Manager.EmployeeId;
                var employeeManagerName = src.Employee.FirstOrDefault(x => x.Id == Manager.EmployeeId) ?? new Employee();
                dest.ManagerName = employeeManagerName.FullName;

                var CoManager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 3) ?? new BranchStoreEmployee();
                dest.CoManagerId = CoManager.EmployeeId;
                var employeeCoManagerName = src.Employee.FirstOrDefault(x => x.Id == CoManager.EmployeeId) ?? new Employee();
                dest.CoManagerName = employeeCoManagerName.FullName;

                var branchStoreAddress = src.Address.FirstOrDefault() ?? new Address();

                dest.Address1 = branchStoreAddress.Address1;
                dest.Address2 = branchStoreAddress.Address2!;
                dest.Street = branchStoreAddress.Street;
                dest.ExternalNumber = branchStoreAddress.ExternalNumber;
                dest.InternalNumber = branchStoreAddress.InternalNumber!;
                dest.City = branchStoreAddress.City;
                dest.ZipCode = branchStoreAddress.ZipCode;
            }
        );

        CreateMap<BranchStore, BranchStoreDetailResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted))
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted)))
        .AfterMap(
            (src, dest) =>
            {
                var Manager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 2) ?? new BranchStoreEmployee();
                dest.ManagerId = Manager.EmployeeId;
                var employeeManagerName = src.Employee.FirstOrDefault(x => x.Id == Manager.EmployeeId) ?? new Employee();
                dest.ManagerName = employeeManagerName.FullName;

                var CoManager = src.BranchStoreEmployee.FirstOrDefault(x => x.JobId == 3) ?? new BranchStoreEmployee();
                dest.CoManagerId = CoManager.EmployeeId;
                var employeeCoManagerName = src.Employee.FirstOrDefault(x => x.Id == CoManager.EmployeeId) ?? new Employee();
                dest.CoManagerName = employeeCoManagerName.FullName;

                var branchStoreAddress = src.Address.FirstOrDefault() ?? new Address();

                dest.Address1 = branchStoreAddress.Address1;
                dest.Address2 = branchStoreAddress.Address2!;
                dest.Street = branchStoreAddress.Street;
                dest.ExternalNumber = branchStoreAddress.ExternalNumber;
                dest.InternalNumber = branchStoreAddress.InternalNumber!;
                dest.City = branchStoreAddress.City;
                dest.ZipCode = branchStoreAddress.ZipCode;
            }
        );

        CreateMap<Drink, DrinkResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

        CreateMap<Employee, EmployeeResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        )
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        )
        .AfterMap(
            (src, dest) =>
            {
                var userAccount = src.UserAccount.FirstOrDefault() ?? new UserAccount();
                dest.UserAccountId = userAccount.Id;
                dest.UserName = userAccount.UserName;

                var initialBranchStore = src.InitialBranchStore ?? new BranchStore();

                dest.BranchStoreName = initialBranchStore.Name;

                var job = src.Job ?? new Job();
                dest.JobName = job.Name;

                var employeeAddress = src.Address.FirstOrDefault() ?? new Address();

                dest.Address1 = employeeAddress.Address1;
                dest.Address2 = employeeAddress.Address2;
                dest.Street = employeeAddress.Street;
                dest.ExternalNumber = employeeAddress.ExternalNumber;
                dest.InternalNumber = employeeAddress.InternalNumber;
                dest.City = employeeAddress.City;
                dest.ZipCode = employeeAddress.ZipCode;
            }
        );

        CreateMap<Rol, BaseCatalogResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        )
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        );

        CreateMap<Job, BaseCatalogResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        )
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        );

        CreateMap<UserAccount, UserAccountResponseDto>()
        .ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.UserName)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        )
        .AfterMap(
            (src, dest) =>
            {

                var rol = src.Rol.FirstOrDefault() ?? new Rol();
                dest.Rol = rol.Name;

                var employee = src.Employee.FirstOrDefault() ?? new Employee();
                dest.EmployeeId = employee.Id;
                dest.FullName = employee.FullName;
                dest.Phone = employee!.Phone!;
                dest.CellPhone = employee.CellPhone;
                dest.Email = employee.Email;
            }
        );

        CreateMap<UserAccount, UserAccountCustomerResponseDto>()
        .ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.UserName)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        )
        .AfterMap(
            (src, dest) =>
            {
                var rol = src.Rol.FirstOrDefault() ?? new Rol();
                dest.Rol = rol.Name;

                var customer = src.Customer.FirstOrDefault() ?? new Customer();
                dest.CustomerId = customer.Id;
                dest.FullName = customer.FullName;
                dest.Phone = customer!.Phone!;
                dest.CellPhone = customer.CellPhone;
                dest.Email = customer.Email!;
            }
        );

        CreateMap<Customer, CustomerResponseDto>()
        .ForMember(
            dest => dest.Name,
            opt => opt.MapFrom(src => $"{src.FirstName} {src.MiddleName} {src.LastName}".Trim()))
        .ForMember(
            dest => dest.Gender,
            opt => opt.MapFrom(src => src.Gender))
        .ForMember(
            dest => dest.GenderName,
            opt => opt.MapFrom(
                src => EnumHelper.GetDescription<Gender>(src.Gender.HasValue ? ((Gender)src.Gender!.Value) : (Gender)(-1))
            )
        )
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => src.Status)
        )
        .ForMember(
            dest => dest.StatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CustomerTypeStatus>((CustomerTypeStatus)src.Status))
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        )
        .AfterMap(
            (src, dest) =>
            {
                var customerAddress = src.CustomerAddress.FirstOrDefault(x => x.CustomerId == src.Id) ?? new CustomerAddress();

                dest.Address1 = customerAddress.Address.Address1;
                dest.Address2 = customerAddress.Address.Address2;
                dest.Street = customerAddress.Address.Street;
                dest.ExternalNumber = customerAddress.Address.ExternalNumber;
                dest.InternalNumber = customerAddress.Address.InternalNumber;
                dest.City = customerAddress.Address.City;
                dest.ZipCode = customerAddress.Address.ZipCode;


            }
        );

        // CreateMap<Address, CustomerResponseDto>();
    }
}