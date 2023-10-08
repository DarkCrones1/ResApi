using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Repositories;

public interface IQueryPagedRepository<T> where T : IBaseQueryable 
{
    Task<IEnumerable<T>> GetPaged(T entity);
}