using System.Linq.Expressions;
using Res.Common.Interfaces.Entities;
using Res.Common.QueryFilters;

namespace Res.Common.Interfaces.Repositories;

public interface IQueryRepository<T>  : IQueryExpresionFilterRepository<T>, IFirstOrDefaultRepository<T> where T : IBaseQueryable
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    //IEnumerable<T> GetBy(T entity);
}