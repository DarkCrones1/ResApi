using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum UserAccountType
{
    [Description("Administrador")]
    Admin = 1,
    [Description("Empleado")]
    Employee = 2,
    [Description("Cliente")]
    Customer = 3
}