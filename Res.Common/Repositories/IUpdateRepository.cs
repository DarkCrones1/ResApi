using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Repositories;
public interface IUpdateRepository<T> where T : IBaseQueryable
{
    void Update(T entity);
}