namespace Res.Domain.Dto.Response;

public class CartDetailResponseDto
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public decimal Total { get; set; }

    public IEnumerable<DrinkResponseDto> Drink { get; } = new List<DrinkResponseDto>();

    public IEnumerable<FoodResponseDto> Food { get; } = new List<FoodResponseDto>();
}