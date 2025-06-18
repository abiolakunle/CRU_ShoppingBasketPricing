using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public class PercentageDiscountOffer : IOffer
    {
        private readonly List<string> _productNames;
        private readonly decimal _percentage;

        public PercentageDiscountOffer(List<string> productNames, decimal percentage)
        {
            _productNames = productNames ?? throw new ArgumentNullException(nameof(productNames));
            if (percentage < 0 || percentage > 100) throw new ArgumentException("Percentage must be between 0 and 100.", nameof(percentage));
            _percentage = percentage;
        }

        public decimal Percentage => _percentage;

        public bool AppliesTo(string productName)
        {
            return _productNames.Contains(productName);
        }

        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            decimal discount = 0;
            foreach (var item in basket)
            {
                if (_productNames.Contains(item.Product.Name))
                {
                    discount += (_percentage / 100) * item.Product.Price * item.Quantity;
                }
            }
            return Math.Round(discount, 2);
        }
    }
}