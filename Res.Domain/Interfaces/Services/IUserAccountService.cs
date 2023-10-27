using System.Linq.Expressions;
using Res.Common.Entities;
using Res.Common.Interfaces.Services;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;

namespace Res.Domain.Interfaces.Services;

public interface IUserAccountService : ICrudService<UserAccount>
{
    Task<ActiveUserAccountEmployee> GetUserAccount(int id);
    Task<ActiveUserAccountEmployee> GetUserAccountToLogin(Expression<Func<ActiveUserAccountEmployee, bool>> filters);
    Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id);
    Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters);
    Task<IEnumerable<BranchStore>> GetBranchStoresToUserAccount(int id);
    Task<int> CreateUser(UserAccount user);
    Task<PagedList<UserAccount>> GetPaged(UserAccountQueryFilter filter);
}