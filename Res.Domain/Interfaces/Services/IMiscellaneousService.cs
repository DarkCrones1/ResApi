using Res.Common.Dtos.Response;

namespace Res.Domain.Interfaces.Services;

public interface IMiscellaneousService
{
    Task<IEnumerable<EnumValueResponseDto>> GetCartStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetCustomerTypeStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetGender();
    Task<IEnumerable<EnumValueResponseDto>> GetOrderDrinkStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetOrderFoodStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetOrderStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetPaymentStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetProductType();
    Task<IEnumerable<EnumValueResponseDto>> GetReservationStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetTicketStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetUserAccountType();
    
}