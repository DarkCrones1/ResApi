using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Payment : BaseRemovableAuditablePaginationEntity
{
    public int BranchStoreId { get; set; }

    public int? CashRegisterId { get; set; }

    public Guid SerialId { get; set; }

    public int TicketId { get; set; }

    public int? CashierId { get; set; }

    public int CustomerId { get; set; }

    public decimal AmountPay { get; set; }

    public decimal AmountRecieve { get; set; }

    public short Status { get; set; }

    public bool? CashPayment { get; set; }

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual BoxCash? CashRegister { get; set; }

    public virtual Employee? Cashier { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}