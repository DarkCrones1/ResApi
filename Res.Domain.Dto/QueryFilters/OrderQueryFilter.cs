using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class OrderQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public int BranchStoreId { get; set; }

    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }
}