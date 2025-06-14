namespace ShoppingBasketPricing.Items
{
    public class CatalogueItem
    {
        public string Name { get; init; }
        public decimal Price { get; init; }

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