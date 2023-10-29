using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum ReservationStatus
{
    [Description("Reservado")]
    Reserved = 1,
    [Description("Pendiente")]
    Pendding = 2,
    [Description("Concretada")]
    Concreted = 3,
    [Description("Cencelada")]
    Canceled = 5
}