using System.ComponentModel;

namespace Res.Domain.Enumerations;

public enum ProductType
{
    [Description("Comida")]
    Food = 1,
    [Description("Bebida")]
    Drink = 2,
    [Description("Postre")]
    Dessert = 3

}