using Res.Common.Entities;

namespace Res.Domain.Entities;

public partial class Customer : BaseRemovableAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public int CustomerTypeId { get; set; }

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual BranchStore CreatedAtBranchStore { get; set; } = null!;

    public virtual ICollection<CustomerAddress> CustomerAddress { get; } = new List<CustomerAddress>();

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<Payment> Payment { get; } = new List<Payment>();

    public virtual ICollection<Reservation> Reservation { get; } = new List<Reservation>();

    public virtual ICollection<Ticket> Ticket { get; } = new List<Ticket>();
}