namespace Res.Domain.Dto.Request.Create;

public class BranchStoreUpdateRequestDto
{
    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Rfc { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? SecondaryPhone { get; set; }

    public int GeographicalZoneId { get; set; }

    public bool IsCentralBranchStore { get; set; }

    // location address
    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Location { get; set; } = null!;
}