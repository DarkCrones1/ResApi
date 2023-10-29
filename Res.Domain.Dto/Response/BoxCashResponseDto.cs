namespace Res.Domain.Dto.Response;

public class BoxCashResponseDto : BaseCatalogResponseDto
{
    public int BranchStoreId { get; set; }

    public string? SerialNumber { get; set; }
}