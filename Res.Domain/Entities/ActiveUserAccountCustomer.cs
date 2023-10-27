using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class ActiveUserAccountCustomer : BaseQueryable
{
    public string UserName { get; set; } = null!;

    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RolId { get; set; }

    public string Name => $"{this.FirstName} {this.MiddleName} {this.LastName}".Trim();
}