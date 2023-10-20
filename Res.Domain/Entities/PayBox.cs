using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class PayBox : BaseRemovableAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int BoxCashId { get; set; }

    public int BranchStoreId { get; set; }

    public int CashierId { get; set; }

    public int TicketId { get; set; }

    public virtual BoxCash BoxCash { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual UserAccount Cashier { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}