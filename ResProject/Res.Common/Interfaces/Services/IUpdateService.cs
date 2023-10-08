using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Services;
public interface IUpdateService<T> where T : IBaseQueryable
{
    Task Update(T entity);
}