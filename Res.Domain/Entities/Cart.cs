using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Cart : BaseAuditablePaginationEntity
{
    public int CustomerId { get; set; }

    public int BranchStoreId { get; set; }

    public short Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual ICollection<Food> Food { get; } = new List<Food>();

    public virtual ICollection<CartDrink> CartDrink { get; } = new List<CartDrink>();

    public virtual ICollection<CartFood> CartFood { get; } = new List<CartFood>();
}