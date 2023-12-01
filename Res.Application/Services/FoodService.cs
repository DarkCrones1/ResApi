using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class FoodService : CatalogBaseService<Food>, IFoodService
{
    public FoodService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Food>> GetPaged(FoodQueryFilter filter)
    {
        var result = await _unitOfWork.FoodRepository.GetPaged(filter);
        var pagedItems = PagedList<Food>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}