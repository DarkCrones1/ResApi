using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class DrinkRepository : CatalogBaseRepository<Drink>, IDrinkRepository
{
    public DrinkRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Drink>> GetPaged(Drink entity)
    {
        var query = _dbContext.Drink.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Drink>> GetPaged(DrinkQueryFilter entity)
    {
        var query = _dbContext.Drink.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));

        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        if (entity.CategoryId != null && entity.CategoryId.Length > 0)
            query = query.Where(x => x.Category.Any(x => entity.CategoryId.Contains(x.Id)));

        if (entity.Price > 0)
            query = query.Where(x => x.Price == entity.Price);

        if (entity.LowPriceRange > 0)
            query = query.Where(x => x.Price >= entity.LowPriceRange);

        if (entity.HighPriceRange > 0)
            query = query.Where(x => x.Price <= entity.HighPriceRange);

        return await query.ToListAsync();
    }
}