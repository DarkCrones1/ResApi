using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Entities;
using System.Linq.Expressions;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Interfaces.Repositories;

    public interface IExistRepository<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
