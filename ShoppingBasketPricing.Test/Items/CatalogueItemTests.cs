using ShoppingBasketPricing.Items;

namespace ShoppingBasketPricing.Test.Items
{
    public class CatalogueItemTests
    {
        [Fact]
        public void CatalogueItem_Constructor_WithInvalidName_ShouldThrowArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new CatalogueItem("", 1.00m));

            // Assert
            Assert.Equal("name", exception.ParamName);
            Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
        }

        [Fact]
        public void CatalogueItem_Constructor_WithNegativePrice_ShouldThrowArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() => new CatalogueItem("Valid Product", -1.00m));

            // Assert
            Assert.Equal("price", exception.ParamName);
            Assert.Contains("Price cannot be negative", exception.Message);
        }
    }
}