using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class CartService : CrudService<Cart>, ICartService
{
    public CartService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Cart>> GetPaged(CartQueryFilter filter)
    {
        var result = await _unitOfWork.CartRepository.GetPaged(filter);
        var pagedItems = PagedList<Cart>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}