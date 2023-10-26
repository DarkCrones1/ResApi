using Res.Common.Entities;
using Res.Common.Interfaces.Services;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Services;

public interface IEmployeeService : ICrudService<Employee>
{
    Task<PagedList<Employee>> GetPaged(EmployeeQueryFilter filter);
}