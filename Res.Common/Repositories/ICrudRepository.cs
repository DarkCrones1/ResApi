using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Repositories;
public interface ICrudRepository<T> : IQueryRepository<T>, ICreateRepository<T>, IUpdateRepository<T>, IDeleteRepository<T>, IExistRepository<T>, IQueryPagedRepository<T> where T : IBaseQueryable
{
}