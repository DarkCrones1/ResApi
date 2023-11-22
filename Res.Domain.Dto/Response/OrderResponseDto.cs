namespace Res.Domain.Dto.Response;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public IEnumerable<OrderDrinkResponseDto> Drink = new List<OrderDrinkResponseDto>();

    public IEnumerable<OrderFoodResponseDto> Food = new List<OrderFoodResponseDto>();
}