using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Rol : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}