using Res.Common.Entities;
using Res.Common.Interfaces.Services;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Services;

public interface IReservationService : ICrudService<Reservation>
{
    Task<PagedList<Reservation>> GetPaged(ReservationQueryFilter filter);
}