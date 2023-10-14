using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class CustomerType : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Customer> Customer { get; } = new List<Customer>();
}