using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Res.Common.Entities;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Services;

namespace Res.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUnitOfWork unitOfWork;

    public AuthenticationService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> IsValidUser(string userName, string password)
    {
        Expression<Func<UserAccount, bool>> filters = x =>
                x.UserName == userName
                && x.Password == password
                && x.IsActive
                && x.IsAuthorized
                && !x.IsDeleted!.Value;

        var result = await unitOfWork.UserAccountRepository.Exist(filters);

        return result;
    }
}