namespace Res.Domain.Dto.Response;

public class BranchStoreResponseDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Rfc { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? SecondaryPhone { get; set; }

    public int? GeographicalZoneId { get; set; }

    public bool? IsCentralBranchStore { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool? IsActive { get; set; } = null;

    public string? ManagerName { get; set; }

    public int ManagerId { get; set; }

    public string? CoManagerName { get; set; }

    public int CoManagerId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;
}