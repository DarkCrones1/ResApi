using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Domain.Dto.QueryFilters;

public class FoodQueryFilter : BaseCatalogQueryFilter
{
    public decimal Price { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public int[]? CategoryId { get; set; }
}