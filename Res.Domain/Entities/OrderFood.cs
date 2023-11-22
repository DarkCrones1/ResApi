using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class OrderFood : BaseEntityPagination
{
    public int FoodId { get; set; }

    public int OrderId { get; set; }

    public short Status { get; set; }

    public virtual Food Food { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}