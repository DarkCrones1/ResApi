using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class BoxCashQueryFilter : BaseCatalogQueryFilter
{
    public int BranchStoreId { get; set; }

    public string? SerialNumber { get; set; }
}