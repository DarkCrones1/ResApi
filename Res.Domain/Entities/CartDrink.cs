using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class CartDrink : BaseEntityPagination
{
    public int CartId { get; set; }

    public int FoodId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Drink Drink { get; set; } = null!;
}