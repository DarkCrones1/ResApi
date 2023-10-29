namespace Res.Domain.Dto.Request.Create;

public class BoxCashBranchStoreCreateRequestDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? SerialNumber { get; set; }
}