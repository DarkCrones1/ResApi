using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Services;

public interface ICreateService<T> where T : IBaseQueryable
{
    Task<int> Create(T entity);

    Task CreateRange(IEnumerable<T> entities);
}
