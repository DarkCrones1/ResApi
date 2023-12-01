namespace Res.Domain.Dto.Response;

public class PaymentResponseDto
{
    public int Id { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public int CashRegisterId { get; set; }

    public string? CashRegisterName { get; set; }

    public Guid SerialId { get; set; }

    public int TicketId { get; set; }

    public int CashierId { get; set; }

    public string? CashierFullName { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public decimal AmountPay { get; set; }

    public decimal AmountRecieve { get; set; }

    public decimal Returnet { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public bool? CashPayment { get; set; }
}