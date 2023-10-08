using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Repositories;
public interface ICreateRepository<T> where T : IBaseQueryable
{
    Task<int> Create(T entity);

    Task CreateRange(IEnumerable<T> entities);
}
