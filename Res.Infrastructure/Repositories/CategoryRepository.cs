using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class CategoryRepository : CatalogBaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Category>> GetPaged(Category entity)
    {
        var query = _dbContext.Category.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetPaged(CategoryQueryFilter entity)
    {
        var query = _dbContext.Category.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (!string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name))
            query = query.Where(x => x.Name.Contains(entity.Name));
        
        if (!string.IsNullOrEmpty(entity.Description) && !string.IsNullOrWhiteSpace(entity.Description))
            query = query.Where(x => x.Description!.Contains(entity.Description));

        if (entity.IsDeleted.HasValue)
            query = query.Where(x => x.IsDeleted == entity.IsDeleted);

        return await query.ToListAsync();
    }
}