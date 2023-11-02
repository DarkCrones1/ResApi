namespace Res.Domain.Dto.Request.Create;

public class UserAccountCustomerCreateRequestDto
{
    // UserAccount
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    // Customer inf
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public int CreatedAtBranchStoreId { get; set; }

    public int CustomerTypeId { get; set; }

    public short? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    // Location Info
    public int[]? BranchStoreIds { get; set; }
}