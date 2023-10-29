using System.Linq.Expressions;

using Res.Common.Interfaces.Repositories;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Repositories;

public interface IUserAccountRepository : IQueryPagedRepository<ActiveUserAccountEmployee>, ICrudRepository<UserAccount>, IQueryFilterPagedRepository<UserAccount, UserAccountQueryFilter>, IQueryFilterPagedRepository<UserAccount, UserAccountCustomerQueryFilter>
{
    Task<ActiveUserAccountEmployee> GetUserAccount(int id);
    Task<IEnumerable<BranchStore>> GetBranchStoresToUser(int id);
    Task<ActiveUserAccountEmployee> GetUserAccountToLogin(Expression<Func<ActiveUserAccountEmployee, bool>> filters);

    Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id);
    Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters);
}