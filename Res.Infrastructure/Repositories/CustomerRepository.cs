using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class CustomerRepository : CrudRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Customer>> GetPaged(Customer entity)
    {
        var query = _dbContext.Customer.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetPaged(CustomerQueryFilter entity)
    {
        var query = _dbContext.Customer.AsQueryable();

        return await query.ToListAsync();
    }
}