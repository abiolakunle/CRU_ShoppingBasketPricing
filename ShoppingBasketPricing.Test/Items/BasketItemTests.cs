using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Test.Items
{
    public class BasketItemTests
    {
        [Fact]
        public void BasketItem_Constructor_WithNullProduct_ShouldThrowArgumentNullException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentNullException>(() => new BasketItem(null, 1));

            // Assert
            Assert.Equal("product", exception.ParamName);
            Assert.Contains("Product cannot be null", exception.Message);
        }

        [Fact]
        public void BasketItem_Constructor_WithNegativeQuantity_ShouldThrowArgumentException()
        {
            // Arrange
            var product = new CatalogueItem("Test Product", 1.00m);

            // Act
            var exception = Assert.Throws<ArgumentException>(() => new BasketItem(product, -1));

            // Assert
            Assert.Equal("quantity", exception.ParamName);
            Assert.Contains("Quantity cannot be negative", exception.Message);
        }
    }
}