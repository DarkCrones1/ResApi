using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Food : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Category> Category { get; } = new List<Category>();

    // public virtual CartFood CartFood { get; set; } = null!;

    public virtual ICollection<OrderFood> OrderFood { get; } = new List<OrderFood>();

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<Menu> Menu { get; } = new List<Menu>();
}