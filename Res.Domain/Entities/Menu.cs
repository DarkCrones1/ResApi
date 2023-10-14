using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Menu : BaseRemovableAuditablePaginationEntity
{
    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual ICollection<Drink> Drink { get; } = new List<Drink>();

    public virtual ICollection<Food> Food { get; } = new List<Food>();
}