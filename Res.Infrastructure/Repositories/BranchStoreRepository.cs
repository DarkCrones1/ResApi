using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class BranchStoreRepository : CrudRepository<BranchStore>, IBranchStoreRepository
{
    public BranchStoreRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<BranchStore>> GetPaged(BranchStore entity)
    {
        var query = _dbContext.BranchStore.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<BranchStore>> GetPaged(BranchStoreQueryFilter entity)
    {
        var query = _dbContext.BranchStore.AsQueryable();

        return await query.ToListAsync();
    }
}