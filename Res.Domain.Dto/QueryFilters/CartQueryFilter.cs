using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class CartQueryFilter : PaginationControlRequestFilter, IBaseQueryFilter
{
    public int Id { get; set; }

    public int BranchStoreId { get; set; }

    public int CustomerId { get; set; }

    public short Status { get; set; }

    public decimal Total { get; set; }

    public decimal MinTotal { get; set; }

    public decimal MaxTotal { get; set; }
}