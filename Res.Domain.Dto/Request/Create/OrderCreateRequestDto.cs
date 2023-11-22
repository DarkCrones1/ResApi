namespace Res.Domain.Dto.Request.Create;

public class OrderCreateRequestDto
{
    public int CustomerId { get; set; }

    public int BranchStoreId { get; set; }

    public int[]? FoodIds { get; set; }

    public int[]? DrinkIds { get; set; }
}