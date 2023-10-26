using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class EmployeeService : CrudService<Employee>, IEmployeeService
{
    public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Employee>> GetPaged(EmployeeQueryFilter filter)
    {
        var result = await _unitOfWork.EmployeeRepository.GetPaged(filter);
        var pagedItems = PagedList<Employee>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}