using Res.Common.Dtos.Response;
using Res.Common.Helpers;
using Res.Domain.Enumerations;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class MiscellaneousService : IMiscellaneousService
{
    public async Task<IEnumerable<EnumValueResponseDto>> GetCartStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CartStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetCustomerTypeStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CustomerTypeStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetGender()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<Gender>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetOrderDrinkStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<OrderDrinkStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetOrderFoodStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<OrderFoodStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetOrderStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<OrderStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetPaymentStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<PaymentStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetProductType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<ProductType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetReservationStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<ReservationStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetTicketStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<TicketStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetUserAccountType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<UserAccountType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }
}