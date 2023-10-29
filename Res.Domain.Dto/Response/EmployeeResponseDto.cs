namespace Res.Domain.Dto.Response;

public class EmployeeResponseDto
{
    public int Id { get; set; }

    public string? BranchStoreName { get; set; }

    public int InitialBranchStoreId { get; set; }

    public string? JobName { get; set; }

    public int JobId { get; set; }

    public int? UserAccountId { get; set; }

    public string? UserName { get; set; }

    public int? RolId { get; set; }

    public string? Rol { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public DateTime? Birthday { get; set; }

    public short? Gender { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Curp { get; set; }

    public string? Rfc { get; set; }

    public string? Ine { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool? IsActive { get; set; } = null;

    public bool? IsDeleted { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;
}