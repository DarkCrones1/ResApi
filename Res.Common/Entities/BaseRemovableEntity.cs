using Res.Common.Interfaces.Entities;

namespace Res.Common.Entities;

public abstract class BaseRemovableEntity : BaseEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}