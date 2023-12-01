using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class ReservationService : CrudService<Reservation>, IReservationService
{
    public ReservationService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Reservation>> GetPaged(ReservationQueryFilter filter)
    {
        var result = await _unitOfWork.ReservationRepository.GetPaged(filter);
        var pagedItems = PagedList<Reservation>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}