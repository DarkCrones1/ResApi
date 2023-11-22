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
        CreateMap<BoxCashCreateRequestDto, BoxCash>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.SerialNumber,
            opt => opt.MapFrom(src => Guid.NewGuid())
        );

        CreateMap<CartCreateRequestDto, Cart>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => (short)CartStatus.Arrange)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                var createdUser = context.Items["CreatedUser"] as string;
                dest.CreatedBy = createdUser;
            }
        );

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
                    City = src.City
                };
                dest.Address.Add(locationAddress);
            }
        );

        CreateMap<BoxCashBranchStoreCreateRequestDto, BoxCash>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        );

        CreateMap<DrinkCreateRequestDto, Drink>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .AfterMap(
            (src, dest) =>
            {
                dest.CreatedBy = "Admin";
            }
        );

        CreateMap<FoodCreateRequestDto, Food>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .AfterMap(
            (src, dest) =>
            {
                dest.CreatedBy = "Admin";
            }
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

        CreateMap<UserAccountCreateRequestDto, UserAccount>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => true)
        )
        .ForMember(
                dest => dest.IsAuthorized,
                opt => opt.MapFrom(src => true) // TODO: este proceso debe de poder realizar la activacion de manera manual
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.AccountType,
            opt => opt.MapFrom(src => (short)UserAccountType.Employee)
        );

        CreateMap<UserAccountCreateRequestDto, Employee>()
            .ForMember(
                dest => dest.IsDeleted,
                opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted))
            .ForMember(
                dest => dest.CreatedDate,
                opt => opt.MapFrom(src => DateTime.Now))
            .AfterMap(
                (src, dest, context) =>
                {
                    // var createdUser = context.Items["CreatedUser"] as string;
                    // dest.CreatedBy = createdUser;

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

                    var brachStoreEmployee = new BranchStoreEmployee
                    {
                        BranchStoreId = src.InitialBranchStoreId!.Value,
                        // CreatedBy = createdUser!,
                        CreatedBy = "Admin",
                        CreatedDate = DateTime.Now,
                        JobId = src.JobId!.Value,
                    };
                    dest.BranchStoreEmployee.Add(brachStoreEmployee);
                }
            );

        CreateMap<CustomerCreateRequestDto, Customer>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => (short)CustomerTypeStatus.New)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                dest.Code = Guid.NewGuid();
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;
                dest.CustomerTypeId = 1;
                dest.CreatedDate = DateTime.Now;
                dest.Status = (short)CustomerTypeStatus.New;
                dest.Gender = src.Gender;
                dest.CustomerTypeId = src.CustomerTypeId;

                // var createdUser = context.Items["CreatedUser"] as string;
                // dest.CreatedBy = createdUser;
            }
        );

        CreateMap<UserAccountCustomerCreateRequestDto, UserAccount>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => true)
        )
        .ForMember(
                dest => dest.IsAuthorized,
                opt => opt.MapFrom(src => true) // TODO: este proceso debe de poder realizar la activacion de manera manual
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.AccountType,
            opt => opt.MapFrom(src => (short)UserAccountType.Customer)
        );

        CreateMap<UserAccountCustomerCreateRequestDto, Customer>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => (short)CustomerTypeStatus.New)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                dest.Code = Guid.NewGuid();
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;
                dest.CustomerTypeId = 1;
                dest.CreatedDate = DateTime.Now;
                dest.Status = (short)CustomerTypeStatus.New;
                dest.Gender = src.Gender;
                dest.CustomerTypeId = src.CustomerTypeId;
                // var createdUser = context.Items["CreatedUser"] as string;
                // dest.CreatedBy = createdUser;
            }
        );

        CreateMap<MenuCreateRequestDto, Menu>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .AfterMap(
            (src, dest, context) =>
            {
                var createdUser = context.Items["CreatedUser"] as string;
                dest.CreatedBy = createdUser;
            }
        );

        CreateMap<OrderCreateRequestDto, Order>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => (short)OrderStatus.Order)
        )
        .AfterMap(
            (src, dest) =>
            {
                if (src.DrinkIds != null)
                {
                    foreach (var item in src.DrinkIds)
                    {
                        var orderDrink = new OrderDrink
                        {
                            DrinkId = item,
                            Status = (short)OrderDrinkStatus.Order,
                        };
                        dest.OrderDrink.Add(orderDrink);
                    }
                }

                if (src.FoodIds != null)
                {
                    foreach (var item in src.FoodIds)
                    {
                        var orderFood = new OrderFood
                        {
                            FoodId = item,
                            Status = (short)OrderFoodStatus.Order
                        };
                        dest.OrderFood.Add(orderFood);
                    }
                }
            }
        );

    }
}