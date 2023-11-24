namespace Res.Domain.Entities;

public partial class Payment
{
    public decimal Returnet
    {
        get
        {
            decimal totalReturn = 0;

            totalReturn = AmountRecieve - AmountPay;

            return totalReturn;
        }
    }
}