using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum CustomerTypeStatus
{
    [Description("Nuevo")]
    New = 1,
    [Description("Casual")]
    Casual = 2,
    [Description("Frecuente")]
    Frequent = 3,
    [Description("VIP")]
    VIP = 4,
    [Description("Problemático")]
    Problematic = 5,
    [Description("Vetado")]
    Banned = 6

}