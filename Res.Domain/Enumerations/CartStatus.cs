using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum CartStatus
{
    [Description("Ordenado")]
    Arrange = 1,
    [Description("Pendiente")]
    Pendding = 2,
    [Description("Proceso")]
    Process = 3,
    [Description("Terminado")]
    Finished = 4,
    [Description("Cancelado")]
    Canceled = 5
}