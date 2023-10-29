namespace Res.Domain.Dto.Response;

public class CategoryResponseDto : BaseCatalogResponseDto
{
    public short ProductType { get; set; }

    public string? ProductTypeName { get; set; }
}