using System.Linq.Expressions;
using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class CustomerService : CrudService<Customer>, ICustomerService
{
    public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Customer>> GetPaged(CustomerQueryFilter filter)
    {
        var result = await _unitOfWork.CustomerRepository.GetPaged(filter);
        var pagedItems = PagedList<Customer>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public async Task Update(int customerId, Customer entity)
    {
        var oldEntity = await _unitOfWork.CustomerRepository.GetById(customerId);
        entity.Code = oldEntity.Code;

        await base.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}