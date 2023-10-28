using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Food : CatalogBaseAuditablePaginationEntity
{
    public decimal Price { get; set; }

    public virtual ICollection<Category> Category { get; } = new List<Category>();

    public virtual ICollection<OrderFood> OrderFood { get; } = new List<OrderFood>();

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<Menu> Menu { get; } = new List<Menu>();
}