using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Job : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<BranchStoreEmployee> BranchStoreEmployee { get; } = new List<BranchStoreEmployee>();

    public virtual ICollection<Employee> Employee { get; } = new List<Employee>();
}