using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;

namespace ShoppingBasketPricing
{
    public interface IBasketPricer
    {
        (decimal subTotal, decimal discount, decimal total) CalculatePrices(List<BasketItem> basket, List<IOffer> offers);
    }
}