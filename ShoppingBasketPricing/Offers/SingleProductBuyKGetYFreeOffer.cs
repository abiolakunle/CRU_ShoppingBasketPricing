using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public class SingleProductBuyKGetYFreeOffer : IOffer
    {
        private readonly Guid _productId;
        private readonly int _buyQuantity;
        private readonly int _freeQuantity;

        public SingleProductBuyKGetYFreeOffer(Guid productId, int buyQuantity, int freeQuantity)
        {
            if (buyQuantity < 1) throw new ArgumentException("Buy quantity must be at least 1.");
            if (freeQuantity < 1) throw new ArgumentException("Free quantity must be at least 1.");
            _productId = productId;
            _buyQuantity = buyQuantity;
            _freeQuantity = freeQuantity;
        }

        public decimal CalculateDiscount(List<BasketItem> basket)
        {
            var item = basket.FirstOrDefault(i => i.Product.Id == _productId);
            if (item != null && item.Quantity > 0)
            {
                int freeItems = (item.Quantity / (_buyQuantity + _freeQuantity)) * _freeQuantity;
                return Math.Round(freeItems * item.Product.Price, 2);
            }
            return 0;
        }
    }
}