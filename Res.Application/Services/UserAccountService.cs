using System.Linq.Expressions;
using Res.Common.Entities;
using Res.Common.Exceptions;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class UserAccountService : CrudService<UserAccount>, IUserAccountService
{
    public UserAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<IEnumerable<BranchStore>> GetBranchStoresToUserAccount(int id)
    {
        var entities = await _unitOfWork.UserAccountRepository.GetBranchStoresToUser(id);
        return entities;
    }

    public async Task<ActiveUserAccountEmployee> GetUserAccount(int id)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccount(id);
        return entity;
    }

    public async Task<ActiveUserAccountEmployee> GetUserAccountToLogin(Expression<Func<ActiveUserAccountEmployee, bool>> filters)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountToLogin(filters);
        return entity;
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomer(int id)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCustomer(id);
        return entity;
    }

    public async Task<ActiveUserAccountCustomer> GetUserAccountCustomerToLogin(Expression<Func<ActiveUserAccountCustomer, bool>> filters)
    {
        var entity = await _unitOfWork.UserAccountRepository.GetUserAccountCustomerToLogin(filters);
        return entity;
    }

    public async Task<int> CreateUser(UserAccount user)
    {
        Expression<Func<UserAccount, bool>> filter = x => x.UserName == user.UserName && !x.IsDeleted!.Value;

        var userAccount = await _unitOfWork.UserAccountRepository.Exist(filter);

        if (userAccount)
            throw new BusinessException("El usuario ya existe");

        await _unitOfWork.UserAccountRepository.Create(user);

        await _unitOfWork.SaveChangesAsync();
        return user.Id;
    }

    public async Task<PagedList<UserAccount>> GetPaged(UserAccountQueryFilter filter)
    {
        var result = await _unitOfWork.UserAccountRepository.GetPaged(filter);
        var pagedItems = PagedList<UserAccount>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}