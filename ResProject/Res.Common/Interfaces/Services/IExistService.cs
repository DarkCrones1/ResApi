using System.Linq.Expressions;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Services;

    public interface IExistService<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
