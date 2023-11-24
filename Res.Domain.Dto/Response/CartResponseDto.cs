namespace Res.Domain.Dto.Response;

public class CartResponseDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public decimal Total { get; set; }
}