using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public class SingleProductBuyKGetYFreeOffer : IOffer
    {
        private readonly string _productName;
        private readonly int _buyQuantity;
        private readonly int _freeQuantity;

        public SingleProductBuyKGetYFreeOffer(string productName, int buyQuantity, int freeQuantity)
        {
            if (buyQuantity < 1) throw new ArgumentException("Buy quantity must be at least 1.");
            if (freeQuantity < 1) throw new ArgumentException("Free quantity must be at least 1.");
            _productName = productName;
            _buyQuantity = buyQuantity;
            _freeQuantity = freeQuantity;
        }

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