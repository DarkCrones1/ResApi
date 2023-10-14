using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Reservation : BaseRemovableAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int BranchStoreId { get; set; }

    public int CustomerId { get; set; }

    public int ManagerId { get; set; }

    public DateTime ReservationTime { get; set; }

    public short Status { get; set; }

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Manager { get; set; } = null!;
}