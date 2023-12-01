namespace Res.Domain.Dto.Request.Create;

public class PaymentCreateRequestDto
{
    public int BranchStoreId { get; set; }

    public int? CashRegisterId { get; set; }

    public int TicketId { get; set; }

    public int? CashierId { get; set; }

    public int CustomerId { get; set; }

    public decimal AmountPay { get; set; }

    public decimal AmountRecieve { get; set; }

    public bool? CashPayment { get; set; }
}