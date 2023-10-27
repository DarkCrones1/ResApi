using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class UserAccountRepository : CrudRepository<UserAccount>, IUserAccountRepository
{
    public UserAccountRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<BranchStore>> GetBranchStoresToUser(int id)
    {
        var query = _dbContext.UserAccount
                    .Where(x => x.Id == id)
                    .Include(x => x.BranchStore)
                    .Select(x => x.BranchStore);
        var entities = await query.FirstOrDefaultAsync();
        return entities ?? new List<BranchStore>();
    }

    public async Task<IEnumerable<ActiveUserAccountEmployee>> GetPaged(ActiveUserAccountEmployee entity)
    {
        var query = _dbContext.ActiveUserAccountEmployee.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<UserAccount>> GetPaged(UserAccountQueryFilter entity)
    {
        var query = _dbContext.UserAccount.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.UserName) && !string.IsNullOrWhiteSpace(entity.UserName))
            query = query.Where(x => x.UserName.Contains(entity.UserName));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }

    public Task<IEnumerable<ActiveUserAccountEmployee>> GetPagedQueryFilter(ActiveUserAccountEmployee entity)
    {
        //TODO: fallo de diseño
        throw new NotImplementedException();
    }

    public async Task<ActiveUserAccountEmployee> GetUserAccount(int id)
    {
        Expression<Func<ActiveUserAccountEmployee, bool>> filter = x => x.Id == id;
        var entity = await GetUserAccountToLogin(filter);

        return entity ?? new ActiveUserAccountEmployee();
    }

    public async Task<ActiveUserAccountEmployee> GetUserAccountToLogin(Expression<Func<ActiveUserAccountEmployee, bool>> filters)
    {
        var entity = await _dbContext.ActiveUserAccountEmployee
        .Where(filters)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        return entity ?? new ActiveUserAccountEmployee();
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id)
    {
        Expression<Func<ActiveUserAccountCustomer, bool>> filter = x => x.Id == id;
        var entity = await GetUserAccountCustomerToLogin(filter);

        return entity ?? new ActiveUserAccountCustomer();
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters)
    {
        var entity = await _dbContext.ActiveUserAccountCustomer
        .Where(filters)
        .AsNoTracking()
        .FirstOrDefaultAsync();

        return entity ?? new ActiveUserAccountCustomer();
    }
}