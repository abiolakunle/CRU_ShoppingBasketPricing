using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    public interface IOffer
    {
        decimal CalculateDiscount(List<BasketItem> basket);
    }
}