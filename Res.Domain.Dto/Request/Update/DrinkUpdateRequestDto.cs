namespace Res.Domain.Dto.Request.Update;

public class DrinkUpdateRequestDto : BaseCatalogUpdateRequestDto
{
    public decimal Price { get; set; }

    public int[]? CategoryIds { get; set; }
}