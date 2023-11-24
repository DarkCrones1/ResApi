namespace Res.Domain.Dto.Request.Update;

public class BoxCashUpdateRequestDto
{
    public string Name { get; set; } = string.Empty;
    public int BranchStoreId { get; set; }
    public string? Description { get; set; }
    public string? InventoryNumber { get; set; }
}