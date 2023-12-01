using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class CategoryService : CatalogBaseService<Category>, ICategoryService
{
    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Category>> GetPaged(CategoryQueryFilter filter)
    {
        var result = await _unitOfWork.CategoryRepository.GetPaged(filter);
        var pagedItems = PagedList<Category>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}