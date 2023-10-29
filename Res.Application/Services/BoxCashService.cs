using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class BoxCashService : CrudService<BoxCash>, IBoxCashService
{
    public BoxCashService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<BoxCash>> GetPaged(BoxCashQueryFilter filter)
    {
        var result = await _unitOfWork.BoxCashRepository.GetPaged(filter);
        var pagedItems = PagedList<BoxCash>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}