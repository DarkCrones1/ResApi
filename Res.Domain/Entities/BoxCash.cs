using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class BoxCash : CatalogBaseAuditablePaginationEntity
{
    public int BranchStoreId { get; set; }

    public string? SerialNumber { get; set; }

    public virtual ICollection<BranchStore> BranchStore { get; } = new List<BranchStore>();

    public virtual ICollection<UserAccount> EmployeePaymentAccount {get;} = new List<UserAccount>();

    public virtual ICollection<Payment> Payment {get; } = new List<Payment>();
}