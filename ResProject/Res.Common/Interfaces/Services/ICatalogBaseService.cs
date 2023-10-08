using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Res.Common.Entities;

namespace Res.Common.Interfaces.Services;

public interface ICatalogBaseService<T> : ICrudService<T> where T : CatalogBaseEntity
{
}