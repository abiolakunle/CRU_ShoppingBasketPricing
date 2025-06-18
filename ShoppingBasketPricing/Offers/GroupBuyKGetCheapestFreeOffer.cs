using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    /// <summary>
    /// Represents an offer where buying a group of products gives the cheapest one for free.
    /// </summary>
    public class GroupBuyKGetCheapestFreeOffer : IOffer
    {
        private readonly List<string> _groupNames;
        private readonly int _buyQuantity;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupBuyKGetCheapestFreeOffer"/> class.
        /// </summary>
        /// <param name="groupNames">The list of product names that form the group.</param>
        /// <param name="buyQuantity">The number of items to buy to qualify for the offer.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="groupNames"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="buyQuantity"/> is less than 1.
        /// </exception>
        public GroupBuyKGetCheapestFreeOffer(List<string> groupNames, int buyQuantity)
        {
            _groupNames = groupNames ?? throw new ArgumentNullException(nameof(groupNames));
            if (buyQuantity < 1) throw new ArgumentException("Buy quantity must be at least 1.");
            _buyQuantity = buyQuantity;
        }

        /// <inheritdoc/>
        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            // Gather prices for all qualifying items, respecting quantity
            var prices = new List<decimal>();
            foreach (var item in basket)
            {
                if (_groupNames.Contains(item.Product.Name) && item.Quantity > 0)
                {
                    var price = item.Product.Price;
                    for (int i = 0; i < item.Quantity; i++)
                        prices.Add(price);
                }
            }
            if (prices.Count == 0) return 0m;

            prices.Sort((a, b) => b.CompareTo(a)); // descending

            int groups = prices.Count / _buyQuantity;
            decimal discount = 0m;
            for (int g = 1; g <= groups; g++)
            {
                discount += prices[g * _buyQuantity - 1]; // Highest price in each group
            }
            return discount;
        }
    }
}