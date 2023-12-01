using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;
public class OrderRepository : CrudRepository<Order>, IOrderRepository
{
    public OrderRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Order>> GetPaged(Order entity)
    {
        var query = _dbContext.Order.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetPaged(OrderQueryFilter entity)
    {
        var query = _dbContext.Order.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.CustomerId > 0)
            query = query.Where(x => x.CustomerId == entity.CustomerId);

        if (entity.CartId > 0)
            query = query.Where(x => x.CartId == entity.Id);

        if (entity.BranchStoreId > 0)
            query = query.Where(x => x.BranchStoreId == entity.BranchStoreId);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        return await query.ToListAsync();
    }
}