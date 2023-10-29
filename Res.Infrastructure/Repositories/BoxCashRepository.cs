using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class BoxCashRepository : CrudRepository<BoxCash>, IBoxCashRepository
{
    public BoxCashRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<BoxCash>> GetPaged(BoxCash entity)
    {
        var query = _dbContext.BoxCash.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<BoxCash>> GetPaged(BoxCashQueryFilter entity)
    {
        var query = _dbContext.BoxCash.AsQueryable();

        return await query.ToListAsync();
    }
}