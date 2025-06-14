using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public class PercentageDiscountOffer : IOffer
    {
        private readonly List<Guid> _productIds;
        private readonly decimal _percentage;

        public PercentageDiscountOffer(List<Guid> productIds, decimal percentage)
        {
            _productIds = productIds ?? throw new ArgumentNullException(nameof(productIds));
            if (percentage < 0 || percentage > 100) throw new ArgumentException("Percentage must be between 0 and 100.", nameof(percentage));
            _percentage = percentage;
        }

        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            decimal discount = 0;
            foreach (var item in basket)
            {
                if (_productIds.Contains(item.Product.Id))
                {
                    discount += (_percentage / 100) * item.Product.Price * item.Quantity;
                }
            }
            return Math.Round(discount, 2);
        }
    }
}