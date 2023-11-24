namespace Res.Domain.Dto.Response;

public class ReservationResponseDto
{
    public int Id { get; set; }

    public Guid SerialId { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public int ManagerId { get; set; }

    public string? ManagerFullName { get; set; }

    public DateTime ReservationTime { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }
}