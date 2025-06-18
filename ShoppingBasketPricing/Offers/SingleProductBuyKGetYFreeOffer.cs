using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    /// <summary>
    /// Represents an offer where buying a certain quantity of a product gives additional units for free.
    /// </summary>
    public class SingleProductBuyKGetYFreeOffer : IOffer
    {
        private readonly string _productName;
        private readonly int _buyQuantity;
        private readonly int _freeQuantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleProductBuyKGetYFreeOffer"/> class.
        /// </summary>
        /// <param name="productName">The name of the product the offer applies to.</param>
        /// <param name="buyQuantity">The quantity to buy to qualify for the offer.</param>
        /// <param name="freeQuantity">The quantity given for free.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="buyQuantity"/> or <paramref name="freeQuantity"/> is less than 1.
        /// </exception>
        public SingleProductBuyKGetYFreeOffer(string productName, int buyQuantity, int freeQuantity)
        {
            if (buyQuantity < 1) throw new ArgumentException("Buy quantity must be at least 1.");
            if (freeQuantity < 1) throw new ArgumentException("Free quantity must be at least 1.");
            _productName = productName;
            _buyQuantity = buyQuantity;
            _freeQuantity = freeQuantity;
        }

        /// <inheritdoc/>
        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            var item = basket.FirstOrDefault(i => i.Product.Name == _productName);
            if (item != null && item.Quantity > 0)
            {
                int freeItems = (item.Quantity / (_buyQuantity + _freeQuantity)) * _freeQuantity;
                return Math.Round(freeItems * item.Product.Price, 2);
            }
            return 0;
        }
    }
}