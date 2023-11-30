using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Ticket : BaseAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int BranchStoreId { get; set; }

    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }

    public DateTime? CloseTicket { get; set; }

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}