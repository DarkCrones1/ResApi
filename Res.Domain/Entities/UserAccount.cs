using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class UserAccount : BaseRemovablePaginationEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsAuthorized { get; set; }

    public DateTime CreatedDate { get; set; }
}