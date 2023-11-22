namespace Res.Domain.Dto.Response;

public class OrderFoodResponseDto
{
    public int Id { get; set; }

    public int FoodId { get; set; }

    public string? FoodName { get; set; }

    public int OrderId { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }
}