using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class BranchStoreEmployee : BaseEntity
{
    public int BranchStoreId { get; set; }

    public int EmployeeId { get; set; }

    public int JobId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Job Job { get; set; } = null!;
}