using Res.Common.Interfaces.Entities;

namespace Res.Common.Entities;

public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public string? LastModifiedBy { get; set; }
}