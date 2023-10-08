using Res.Common.Interfaces.Entities;

namespace Res.Common.Entities;

public abstract class BaseEntity : IBaseQueryable
{
    public int Id { get; set; }
}