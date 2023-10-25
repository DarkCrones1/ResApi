namespace Res.Domain.Dto.Request.Create;

public class UserAccountCreateRequestDto
{
    // UserAccount
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    // EmployeeId
    public int? EmployeeId { get; set; }

    // Employee info
    public int? InitialBranchStoreId { get; set; }

    public int? JobId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public DateTime? Birthday { get; set; }

    public short? Gender { get; set; }

    public string? Phone { get; set; }

    public string? CellPhone { get; set; }

    public string? Email { get; set; }

    public string? Curp { get; set; }

    public string? Rfc { get; set; }

    public string? Ine { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string ZipCode { get; set; } = null!;

    public string? City { get; set; } = null!;

    // Location Info
    public int[]? BranchStoreIds { get; set; }

    // User Rol Info
    public int[]? RolIds { get; set; }

}