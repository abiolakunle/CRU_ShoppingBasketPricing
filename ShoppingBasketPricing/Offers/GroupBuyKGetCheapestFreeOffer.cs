using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public class GroupBuyKGetCheapestFreeOffer : IOffer
    {
        private readonly List<Guid> _groupIds;
        private readonly int _buyQuantity;

        public GroupBuyKGetCheapestFreeOffer(List<Guid> groupIds, int buyQuantity)
        {
            _groupIds = groupIds ?? throw new ArgumentNullException(nameof(groupIds));
            if (buyQuantity < 1) throw new ArgumentException("Buy quantity must be at least 1.");
            _buyQuantity = buyQuantity;
        }

        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            // Gather prices for all qualifying items, respecting quantity
            var prices = new List<decimal>();
            foreach (var item in basket)
            {
                if (_groupIds.Contains(item.Product.Id) && item.Quantity > 0)
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