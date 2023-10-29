namespace Res.Domain.Dto.Request.Create;

public class FoodUpdateRequestDto : BaseCatalogUpdateRequestDto
{
    public decimal Price { get; set; }

    public int[]? CategoryIds { get; set; }
}