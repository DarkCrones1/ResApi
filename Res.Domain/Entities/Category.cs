using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Category : CatalogBaseAuditablePaginationEntity
{
    public short ProductType { get; set; }

    public virtual ICollection<Food> Food { get; set; } = new List<Food>();

    public virtual ICollection<Drink> Drink { get; set; } = new List<Drink>();
}