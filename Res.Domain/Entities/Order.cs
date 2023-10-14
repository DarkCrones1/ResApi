using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Order : BaseEntityPagination
{
    public int BranchStoreId { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual ICollection<OrderDrink> OrderDrink { get; } = new List<OrderDrink>();

    public virtual ICollection<OrderFood> OrderFood { get; } = new List<OrderFood>();
}