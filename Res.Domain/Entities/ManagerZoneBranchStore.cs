using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class ManagerZoneBranchStore : BaseEntityPagination
{
    public int BranchStoreId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual BranchStore BranchStore { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}