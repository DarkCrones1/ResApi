using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum TicketStatus
{
    [Description("Sin Pagar")]
    Pendding = 1,
    [Description("Pagado")]
    Payment = 2,
    [Description("Cancelado")]
    Canceled = 5
}