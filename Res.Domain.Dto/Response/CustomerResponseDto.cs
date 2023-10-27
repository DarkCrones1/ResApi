namespace Res.Domain.Dto.Response;

public class CustomerResponseDto
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public int CustomerTypeId { get; set; }

    public int CreatedAtBranchStoreId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;
}