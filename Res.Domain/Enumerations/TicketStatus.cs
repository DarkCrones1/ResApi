using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum TicketStatus
{
    [Description("Pendiente")]
    Pendding = 1,
    [Description("Pagado")]
    Payment = 2,
    [Description("Cancelado")]
    Canceled = 5
}