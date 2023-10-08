using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class PayBox : BaseRemovableAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int BoxCashId { get; set; }

    public int EmployeeId { get; set; }

    public int TicketId { get; set; }

    public decimal AmountPay { get; set; }

    public decimal AmountRecieve { get; set; }

    public decimal AmountReturnet
    {
        get
        {
            var change = AmountRecieve - AmountPay;
            return change;
        }
    }

    public virtual BoxCash BoxCash { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual UserAccount Employee { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}