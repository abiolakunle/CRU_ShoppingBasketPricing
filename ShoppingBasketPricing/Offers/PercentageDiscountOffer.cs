using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    /// <summary>
    /// Represents an offer that applies a percentage discount to specific products.
    /// </summary>
    public class PercentageDiscountOffer : IOffer
    {
        private readonly List<string> _productNames;
        private readonly decimal _percentage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentageDiscountOffer"/> class.
        /// </summary>
        /// <param name="productNames">The list of product names to which the discount applies.</param>
        /// <param name="percentage">The percentage discount (0-100).</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="productNames"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="percentage"/> is not between 0 and 100.
        /// </exception>
        public PercentageDiscountOffer(List<string> productNames, decimal percentage)
        {
            _productNames = productNames ?? throw new ArgumentNullException(nameof(productNames));
            if (percentage < 0 || percentage > 100) throw new ArgumentException("Percentage must be between 0 and 100.", nameof(percentage));
            _percentage = percentage;
        }

        /// <summary>
        /// Gets the percentage discount for this offer.
        /// </summary>
        public decimal Percentage => _percentage;

        /// <summary>
        /// Determines whether this offer applies to the specified product name.
        /// </summary>
        /// <param name="productName">The product name to check.</param>
        /// <returns>True if the offer applies; otherwise, false.</returns>
        public bool AppliesTo(string productName)
        {
            return _productNames.Contains(productName);
        }

        /// <inheritdoc/>
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