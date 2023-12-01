using Res.Common.Entities;
using Res.Common.Interfaces.Services;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Services;

public interface IMenuService : ICatalogBaseService<Menu>
{
    Task<PagedList<Menu>> GetPaged(MenuQueryFilter filter);
}