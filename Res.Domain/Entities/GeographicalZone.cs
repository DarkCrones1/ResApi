using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class GeographicalZone : CatalogBaseAuditablePaginationEntity
{
    public string Code { get; set; } = null!;

    public virtual ICollection<BranchStore> BranchStore { get; } = new List<BranchStore>();
}