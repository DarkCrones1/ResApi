using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class CategoryQueryFilter : BaseCatalogQueryFilter
{
    public short ProductType { get; set; }
}