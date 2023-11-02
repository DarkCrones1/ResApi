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

    public int CreatedAtBranchStoreId { get; set; }

    public short Status { get; set; }

    public short? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual BranchStore CreatedAtBranchStore { get; set; } = null!;

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<Order> Order { get; } = new List<Order>();

    public virtual ICollection<Payment> Payment { get; } = new List<Payment>();

    public virtual ICollection<Reservation> Reservation { get; } = new List<Reservation>();

    public virtual ICollection<Ticket> Ticket { get; } = new List<Ticket>();

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}