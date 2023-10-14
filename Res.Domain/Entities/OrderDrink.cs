using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class OrderDrink : BaseEntityPagination
{
    public int DrinkId { get; set; }

    public int OrderId { get; set; }

    public short Status { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Drink Drink { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}