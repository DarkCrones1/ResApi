namespace Res.Domain.Dto.Request.Create;

public class CartCreateRequestDto
{
    public int CustomerId { get; set; }

    public int BranchStoreId { get; set; }

    public int[]? FoodIds { get; set; }

    public int[]? DrinkIds { get; set; }
}