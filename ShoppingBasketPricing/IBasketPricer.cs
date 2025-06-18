using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;

namespace ShoppingBasketPricing
{
    /// <summary>
    /// Defines a contract for calculating basket prices, including subtotal, discount, and total.
    /// </summary>
    public interface IBasketPricer
    {
        /// <summary>
        /// Calculates the subtotal, total discount, and final total for a basket with the given offers.
        /// </summary>
        /// <param name="basket">The list of items in the basket.</param>
        /// <param name="offers">The list of offers to apply.</param>
        /// <returns>A tuple containing the subtotal, total discount, and final total.</returns>
        (decimal subTotal, decimal discount, decimal total) CalculatePrices(List<BasketItem> basket, List<IOffer> offers);
    }
}