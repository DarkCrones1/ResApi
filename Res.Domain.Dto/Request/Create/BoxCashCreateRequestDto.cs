namespace Res.Domain.Dto.Request.Create;

public class BoxCashCreateRequestDto
{
    public string Name { get; set; } = string.Empty;

    public int BranchStoreId { get; set; }

    public string? Description { get; set; }

    public string? InventoryNumber { get; set; }

    public string? SerialNumber { get; set; }
}