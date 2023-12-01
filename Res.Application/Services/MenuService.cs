using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class MenuService : CrudService<Menu>, IMenuService
{
    public MenuService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Menu>> GetPaged(MenuQueryFilter filter)
    {
        var result = await _unitOfWork.MenuRepository.GetPaged(filter);
        var pagedItems = PagedList<Menu>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}