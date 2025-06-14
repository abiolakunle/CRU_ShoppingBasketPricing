namespace ShoppingBasketPricing.Items
{
    public class BasketItem
    {
        public CatalogueItem Product { get; init; }
        public int Quantity { get; init; }

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