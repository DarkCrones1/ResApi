namespace Res.Domain.Dto.Request.Create;

public class FoodCreateRequestDto : BaseCatalogCreateRequestDto
{
    public decimal Price { get; set; }

    public int[]? CategoryIds { get; set; } 
}