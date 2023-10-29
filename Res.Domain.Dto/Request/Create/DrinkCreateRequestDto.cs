namespace Res.Domain.Dto.Request.Create;

public class DrinkCreateRequestDto : BaseCatalogCreateRequestDto
{
    public decimal Price { get; set; }

    public int[]? CategoryIds { get; set; } 

}