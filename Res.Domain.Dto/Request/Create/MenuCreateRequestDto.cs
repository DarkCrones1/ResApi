namespace Res.Domain.Dto.Request.Create;

public class MenuCreateRequestDto : BaseCatalogCreateRequestDto
{
    public int BranchStoreId { get; set; }

    public int[]? DrinkIds { get; set; }

    public int[]? FoodIds { get; set; }
}