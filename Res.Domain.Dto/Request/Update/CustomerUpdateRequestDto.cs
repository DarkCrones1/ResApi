namespace Res.Domain.Dto.Request.Update;

public class CustomerUpdateRequestDto
{
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public int CreatedAtBranchStoreId { get; set; }

    // public string Address1 { get; set; } = null!;

    // public string? Address2 { get; set; }

    // public string Street { get; set; } = null!;

    // public string ExternalNumber { get; set; } = null!;

    // public string? InternalNumber { get; set; }

    // public string City { get; set; } = null!;

    // public string ZipCode { get; set; } = null!;

    public int CustomerTypeId { get; set; }

    public short? Gender { get; set; }

    public short? Status { get; set; }

    public DateTime? BirthDate { get; set; }
}