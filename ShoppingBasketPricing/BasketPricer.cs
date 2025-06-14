using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;

namespace ShoppingBasketPricing
{
    public class BasketPricer : IBasketPricer
    {
        public (decimal subTotal, decimal discount, decimal total) CalculatePrices(List<BasketItem> basket, List<IOffer> offers)
        {
            decimal subTotal = Math.Round(basket.Sum(item => item.Product.Price * item.Quantity), 2, MidpointRounding.ToEven);
            decimal discount = Math.Round(offers.Sum(offer => offer.CalculateDiscount(basket)), 2, MidpointRounding.ToEven);
            decimal total = Math.Round(subTotal - discount, 2, MidpointRounding.ToEven);

            return (subTotal, discount, total < 0 ? 0m : total);
        }
    }
}