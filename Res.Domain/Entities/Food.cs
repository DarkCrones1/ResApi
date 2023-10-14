using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Food : CatalogBaseAuditablePaginationEntity
{
    public int CategoryId { get; set; }

    public virtual ICollection<Category> Category { get; } = new List<Category>();

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<Menu> Menu { get; } = new List<Menu>();
}