using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class MenuRepository : CrudRepository<Menu>, IMenuRepository
{
    public MenuRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Menu>> GetPaged(Menu entity)
    {
        var query = _dbContext.Menu.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Menu>> GetPaged(MenuQueryFilter entity)
    {
        var query = _dbContext.Menu.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        if (entity.BranchStoreId > 0)
            query = query.Where(x => x.BranchStoreId == entity.BranchStoreId);

        return await query.ToListAsync();
    }
}