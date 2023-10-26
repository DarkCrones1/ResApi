using System.Linq.Expressions;

using Res.Common.Interfaces.Repositories;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Repositories;

public interface IUserAccountRepository : IQueryPagedRepository<ActiveUserAccountEmployee>, ICrudRepository<UserAccount>, IQueryFilterPagedRepository<UserAccount, UserAccountQueryFilter>
{
    Task<ActiveUserAccountEmployee> GetUserAccount(int id);
    Task<ActiveUserAccountEmployee> GetUserAccountToLogin(Expression<Func<ActiveUserAccountEmployee, bool>> filters);
    Task<IEnumerable<BranchStore>> GetBranchStoresToUser(int id);
}