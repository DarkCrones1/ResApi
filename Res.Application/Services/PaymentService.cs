using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class PaymentService : CrudService<Payment>, IPaymentService
{
    public PaymentService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Payment>> GetPaged(PaymentQueryFilter filter)
    {
        var result = await _unitOfWork.PaymentRepository.GetPaged(filter);
        var pagedItems = PagedList<Payment>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}