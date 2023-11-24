namespace Res.Domain.Dto.Request.Update;

public class MenuUpdateRequestDto : BaseCatalogUpdateRequestDto
{
    public int BranchStoreId { get; set; }

    public int[]? DrinkIds { get; set; }

    public int[]? FoodIds { get; set; }
}