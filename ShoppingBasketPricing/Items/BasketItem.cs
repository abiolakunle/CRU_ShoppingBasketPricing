namespace ShoppingBasketPricing.Items
{
    /// <summary>
    /// Represents an item in the shopping basket, including the product and its quantity.
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// Gets the product associated with this basket item.
        /// </summary>
        public CatalogueItem Product { get; init; }

        /// <summary>
        /// Gets the quantity of the product in the basket.
        /// </summary>
        public int Quantity { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasketItem"/> class.
        /// </summary>
        /// <param name="product">The product to add to the basket.</param>
        /// <param name="quantity">The quantity of the product.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="product"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="quantity"/> is negative.</exception>
        public BasketItem(CatalogueItem product, int quantity)
        {
            ValidateProduct(product);
            Product = product;
            ValidateQuantity(quantity);
            Quantity = quantity;
        }

        private static void ValidateProduct(CatalogueItem product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));
        }
    }
}