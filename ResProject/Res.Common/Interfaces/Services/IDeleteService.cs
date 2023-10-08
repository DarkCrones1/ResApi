using System.Linq.Expressions;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Services;
public interface IDeleteService<T> where T : IBaseQueryable
{
    Task<int> Delete(int id);
    Task<int> DeleteRange(IEnumerable<int> idList);
    Task<int> DeleteBy(Expression<Func<T, bool>> filter);
}
