namespace ShoppingBasketPricing.Items
{
    /// <summary>
    /// Represents a product in the catalogue with a name and price.
    /// </summary>
    public class CatalogueItem
    {
        /// <summary>
        /// Gets the name of the product.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueItem"/> class.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="price">The price of the product.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="name"/> is null, empty, or whitespace, or if <paramref
        /// name="price"/> is negative.
        /// </exception>
        public CatalogueItem(string name, decimal price)
        {
            ValidateName(name);
            Name = name.Trim();
            ValidatePrice(price);
            Price = price;
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null, empty, or whitespace.", nameof(name));
        }

        private static void ValidatePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Price cannot be negative.", nameof(price));
        }
    }
}