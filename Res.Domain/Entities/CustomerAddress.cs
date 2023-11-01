using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class CustomerAddress : BaseEntity
{
    public int CustomerId { get; set; }

    public int AddressId { get; set; }

    public DateTime RegisterDate { get; set; }

    public short Status { get; set; }

    // public virtual Address Address { get; set; } = null!;

    // public virtual Customer Customer { get; set; } = null!;
}