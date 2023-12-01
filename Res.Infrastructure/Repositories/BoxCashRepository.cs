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

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (!string.IsNullOrEmpty(entity.SerialNumber) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.SerialNumber));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        if (entity.BranchStoreId > 0)
            query = query.Where(x => x.BranchStoreId == entity.Id);

        return await query.ToListAsync();
    }
}