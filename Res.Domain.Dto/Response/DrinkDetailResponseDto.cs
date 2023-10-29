namespace Res.Domain.Dto.Response;

public class DrinkDetailResponseDto : BaseCatalogResponseDto
{
    public decimal Price { get; set; }

    public IEnumerable<CategoryResponseDto> Category { get; } = new List<CategoryResponseDto>();
}