namespace Res.Domain.Dto.Response;

public class BranchStoreDetailResponseDto
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

    public string ManagerName { get; set; } = string.Empty;

    public int ManagerId { get; set; }

    public string CoManagerName { get; set; } = string.Empty;

    public int CoManagerId { get; set; }

    public string Address1 { get; set; } = string.Empty;

    public string Address2 { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;

    public string ExternalNumber { get; set; } = string.Empty;

    public string InternalNumber { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public IEnumerable<EmployeeResponseDto> Employee { get; } = new List<EmployeeResponseDto>();
}