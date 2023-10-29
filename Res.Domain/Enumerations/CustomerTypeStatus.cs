using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum CustomerTypeStatus
{
    [Description("Nuevo")]
    New = 1,
    [Description("Viejo")]
    Old = 2
}