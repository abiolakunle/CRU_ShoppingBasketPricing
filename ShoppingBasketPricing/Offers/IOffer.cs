using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Offers
{
    /// <summary>
    /// Represents a discount offer that can be applied to a shopping basket.
    /// </summary>
    public interface IOffer
    {
        /// <summary>
        /// Calculates the discount amount for the given basket.
        /// </summary>
        /// <param name="basket">The list of items in the basket.</param>
        /// <returns>The discount amount to be applied.</returns>
        decimal CalculateDiscount(List<BasketItem> basket);
    }
}