using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class ReservationRepository : CrudRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Reservation>> GetPaged(Reservation entity)
    {
        var query = _dbContext.Reservation.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetPaged(ReservationQueryFilter entity)
    {
        var query = _dbContext.Reservation.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.BranchStoreId > 0)
            query = query.Where(x => x.BranchStoreId == entity.BranchStoreId);

        if (entity.SerialId.HasValue)
            if (entity.SerialId != Guid.Empty)
                query = query.Where(x => x.SerialId == entity.SerialId);

        if (entity.ManagerId > 0)
            query = query.Where(x => x.ManagerId == entity.ManagerId);

        if (entity.CustomerId > 0)
            query = query.Where(x => x.CustomerId == entity.CustomerId);

        if (entity.ReservationTime.HasValue)
            query = query.Where(x => x.ReservationTime == entity.ReservationTime.Value);

        if (entity.MinReservationTime.HasValue)
            query = query.Where(x => x.ReservationTime == entity.MinReservationTime.Value);

        if (entity.MaxReservationTime.HasValue)
            query = query.Where(x => x.ReservationTime == entity.MaxReservationTime.Value);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        return await query.ToListAsync();
    }
}