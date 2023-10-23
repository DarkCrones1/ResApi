using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class BranchStoreService : CrudService<BranchStore>, IBranchStoreService
{
    public BranchStoreService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<BranchStore>> GetPaged(BranchStoreQueryFilter filter)
    {
        var result = await _unitOfWork.BranchStoreRepository.GetPaged(filter);
        var pagedItems = PagedList<BranchStore>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}