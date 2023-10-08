using System.ComponentModel.DataAnnotations.Schema;
using Res.Common.Interfaces.Entities;

namespace Res.Common.Entities;
public abstract class BaseEntityPagination : BaseEntity, IPaginationQueryable
{
    [NotMapped]
    public int PageSize { get; set; }

    [NotMapped]
    public int PageNumber { get; set; }
}
