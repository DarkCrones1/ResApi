using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class EmployeeRepository : CrudRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Employee>> GetPaged(Employee entity)
    {
        var query = _dbContext.Employee.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Employee>> GetPaged(EmployeeQueryFilter entity)
    {
        var query = _dbContext.Employee.AsQueryable();

        return await query.ToListAsync();
    }
}