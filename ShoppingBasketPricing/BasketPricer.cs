using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;

namespace ShoppingBasketPricing
{
    public class BasketPricer : IBasketPricer
    {
        public (decimal subTotal, decimal discount, decimal total) CalculatePrices(List<BasketItem> basket, List<IOffer> offers)
        {
            decimal subTotal = Math.Round(basket.Sum(item => item.Product.Price * item.Quantity), 2, MidpointRounding.ToEven);

            // Handle sequential percentage discounts
            var percentageOffers = offers.OfType<PercentageDiscountOffer>().ToList();
            var otherOffers = offers.Except(percentageOffers).ToList();

            decimal percentageDiscount = 0m;
            foreach (var item in basket)
            {
                var applicablePercentages = percentageOffers
                    .Where(o => o.AppliesTo(item.Product.Name))
                    .Select(o => o.Percentage)
                    .ToList();

                if (applicablePercentages.Count > 0)
                {
                    decimal originalPrice = item.Product.Price;
                    decimal discountedPrice = originalPrice;
                    foreach (var pct in applicablePercentages)
                    {
                        discountedPrice *= (1 - pct / 100m);
                    }
                    decimal itemDiscount = (originalPrice - discountedPrice) * item.Quantity;
                    percentageDiscount += Math.Round(itemDiscount, 2, MidpointRounding.ToEven);
                }
            }

            // Calculate other discounts as before
            decimal otherDiscount = Math.Round(otherOffers.Sum(offer => offer.CalculateDiscount(basket)), 2, MidpointRounding.ToEven);

            decimal totalDiscount = Math.Round(percentageDiscount + otherDiscount, 2, MidpointRounding.ToEven);
            decimal total = Math.Round(subTotal - totalDiscount, 2, MidpointRounding.ToEven);

            return (subTotal, totalDiscount, total < 0 ? 0m : total);
        }
    }
}