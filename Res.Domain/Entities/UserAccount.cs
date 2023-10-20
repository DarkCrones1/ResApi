using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class UserAccount : BaseRemovablePaginationEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsAuthorized { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<BranchStore> BranchStore { get; } = new List<BranchStore>();

    public virtual ICollection<Customer> Customer { get; } = new List<Customer>();

    public virtual ICollection<Employee> Employee { get; } = new List<Employee>();

    public virtual ICollection<PayBox> PayBox { get; } = new List<PayBox>();

    public virtual ICollection<Rol> Rol { get; } = new List<Rol>();
}