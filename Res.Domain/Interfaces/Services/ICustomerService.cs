using Res.Common.Entities;
using Res.Common.Interfaces.Services;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Services;

public interface ICustomerService : ICrudService<Customer>
{
    Task<PagedList<Customer>> GetPaged(CustomerQueryFilter filter);
    Task Update(int customerId, Customer entity);
}