namespace Res.Domain.Dto.Response;

public class OrderDrinkResponseDto
{
    public int Id { get; set; }

    public int DrinkId { get; set; }

    public string? DrinkName { get; set; }

    public int OrderId { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }
}