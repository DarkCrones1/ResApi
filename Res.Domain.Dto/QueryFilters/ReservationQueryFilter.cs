using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class ReservationQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public Guid? SerialId { get; set; }

    public int BranchStoreId { get; set; }

    public int CustomerId { get; set; }

    public int ManagerId { get; set; }

    public DateTime? ReservationTime { get; set; }
    
    public DateTime? MinReservationTime { get; set; }

    public DateTime? MaxReservationTime { get; set; }

    public short Status { get; set; }
}