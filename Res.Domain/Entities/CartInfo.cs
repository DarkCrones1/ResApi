namespace Res.Domain.Entities;

public partial class Cart
{
    public decimal Total
    {
        get
        {
            decimal totalPriceDrink = 0; // Inicializamos la variable totalPriceDrink
            decimal totalPriceFood = 0;  // Inicializamos la variable totalPriceFood

            // Calculamos el total de las bebidas
            foreach (var item in Drink)
            {
                totalPriceDrink += item.Price;
            }

            // Calculamos el total de los alimentos
            foreach (var item in Food)
            {
                totalPriceFood += item.Price;
            }

            decimal totalPrice = totalPriceDrink + totalPriceFood; // Calculamos el total general
            return totalPrice; // Devolvemos el total
        }
    }
}
