using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;
using Res.Domain.Dto.QueryFilters;

namespace Res.Infrastructure.Repositories;

public class PaymentRepository : CrudRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(ResDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Payment>> GetPaged(Payment entity)
    {
        var query = _dbContext.Payment.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetPaged(PaymentQueryFilter entity)
    {
        var query = _dbContext.Payment.AsQueryable();

        if (entity.Id > 0)
            query = query.Where(x => x.Id == entity.Id);

        if (entity.BranchStoreId > 0)
            query = query.Where(x => x.BranchStoreId == entity.BranchStoreId);

        if (entity.CashRegisterId > 0)
            query = query.Where(x => x.CashRegisterId == entity.CashRegisterId);

        if (entity.SerialId.HasValue)
            if (entity.SerialId != Guid.Empty)
                query = query.Where(x => x.SerialId == entity.SerialId);

        if (entity.TicketId > 0)
            query = query.Where(x => x.TicketId == entity.TicketId);

        if (entity.CashierId > 0)
            query = query.Where(x => x.CashierId == entity.CashierId);

        if (entity.CustomerId > 0)
            query = query.Where(x => x.CustomerId == entity.CustomerId);

        if (entity.Status > 0)
            query = query.Where(x => x.Status == entity.Status);

        if (entity.AmountPay > 0)
            query = query.Where(x => x.AmountPay == entity.AmountPay);

        if (entity.MinAmountPay > 0)
            query = query.Where(x => x.AmountPay >= entity.MinAmountPay);

        if (entity.MaxAmountPay > 0)
            query = query.Where(x => x.AmountPay <= entity.MaxAmountPay);

        if (entity.AmountRecieve > 0)
            query = query.Where(x => x.AmountRecieve == entity.AmountRecieve);

        if (entity.MinAmountRecieve > 0)
            query = query.Where(x => x.AmountRecieve >= entity.MinAmountRecieve);

        if (entity.MaxAmountRecieve > 0)
            query = query.Where(x => x.AmountRecieve <= entity.MaxAmountRecieve);

        return await query.ToListAsync();
    }
}