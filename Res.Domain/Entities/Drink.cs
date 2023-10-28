using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Drink : CatalogBaseAuditablePaginationEntity
{
    public decimal Price { get; set; }

    public virtual ICollection<Category> Category { get; } = new List<Category>();

    public virtual ICollection<OrderDrink> OrderDrink { get; } = new List<OrderDrink>();

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<Menu> Menu { get; } = new List<Menu>();
}