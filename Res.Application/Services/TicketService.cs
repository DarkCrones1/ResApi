using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;
public class TicketService : CrudService<Ticket>, ITicketService
{
    public TicketService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Ticket>> GetPaged(TicketQueryFilter filter)
    {
        var result = await _unitOfWork.TicketRepository.GetPaged(filter);
        var pagedItems = PagedList<Ticket>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}