namespace Res.Domain.Dto.Request.Create;

public class ReservationCreateRequestDto
{
    public int BranchStoreId { get; set; }

    public int CustomerId { get; set; }

    public int ManagerId { get; set; }

    public DateTime ReservationTime { get; set; }
}