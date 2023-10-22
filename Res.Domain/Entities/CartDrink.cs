using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class CartDrink : BaseEntityPagination
{
    public int CartId { get; set; }

    public int DrinkId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Cart Cart { get; set; } = null!;

    public virtual Drink Drink { get; set; } = null!;
}