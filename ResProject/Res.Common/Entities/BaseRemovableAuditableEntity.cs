using Res.Common.Interfaces.Entities;

namespace Res.Common.Entities;

public abstract class BaseRemovableAuditableEntity : BaseAuditableEntity, IRemovableEntity
{
    public bool? IsDeleted { get; set; }
}