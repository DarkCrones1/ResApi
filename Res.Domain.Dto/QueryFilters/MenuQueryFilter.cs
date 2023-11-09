using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class MenuQueryFilter : BaseCatalogQueryFilter
{
    public int BranchStoreId { get; set; }
}