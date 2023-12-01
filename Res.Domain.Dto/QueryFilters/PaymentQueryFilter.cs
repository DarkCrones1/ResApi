using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class PaymentQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }
    
    public int BranchStoreId { get; set; }

    public int CashRegisterId { get; set; }

    public Guid? SerialId { get; set; }

    public int TicketId { get; set; }

    public int CashierId { get; set; }

    public int CustomerId { get; set; }

    public decimal AmountPay { get; set; }

    public decimal MinAmountPay { get; set; }

    public decimal MaxAmountPay { get; set; }

    public decimal AmountRecieve { get; set; }

    public decimal MinAmountRecieve { get; set; }

    public decimal MaxAmountRecieve { get; set; }

    public short Status { get; set; }
}