using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;
public class OrderService : CrudService<Order>, IOrderService
{
    public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Order>> GetPaged(OrderQueryFilter filter)
    {
        var result = await _unitOfWork.OrderRepository.GetPaged(filter);
        var pagedItems = PagedList<Order>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}