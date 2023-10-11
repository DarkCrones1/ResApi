using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Employee : BaseRemovableAuditablePaginationEntity
{
    public int InitialBranchStoreId { get; set; }

    public int JobId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime? Birthday { get; set; }

    public short? Gender { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Curp { get; set; }

    public string? Ine { get; set; }

    public virtual ICollection<Address> Address { get; } = new List<Address>();

    public virtual ICollection<BranchStoreEmployee> BranchStoreEmployee { get; } = new List<BranchStoreEmployee>();

    public virtual BranchStore InitialBranchStore { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;

    public virtual ICollection<ManagerZoneBranchStore> ManagerZoneBranchStore { get; } = new List<ManagerZoneBranchStore>();

    public virtual ICollection<Payment> Payment { get; } = new List<Payment>();

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}