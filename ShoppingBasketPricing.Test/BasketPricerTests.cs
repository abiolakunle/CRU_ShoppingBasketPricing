using ShoppingBasketPricing.Items;
using ShoppingBasketPricing.Offers;

namespace ShoppingBasketPricing.Test
{
    public class BasketPricerTests
    {
        [Fact]
        public void EmptyBasket_ShouldReturnZero()
        {
            var basket = new List<BasketItem>();
            var offers = new List<IOffer>();
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(0m, result.subTotal);
            Assert.Equal(0m, result.discount);
            Assert.Equal(0m, result.total);
        }

        [Fact]
        public void SingleProduct_NoOffers_ShouldReturnPrice()
        {
            var product = new CatalogueItem("Biscuits", 1.20m);
            var basket = new List<BasketItem> { new BasketItem(product, 1) };
            var offers = new List<IOffer>();
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(1.20m, result.subTotal);
            Assert.Equal(0m, result.discount);
            Assert.Equal(1.20m, result.total);
        }

        [Fact]
        public void MultipleProducts_NoOffers_ShouldReturnSum()
        {
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var biscuits = new CatalogueItem("Biscuits", 1.20m);
            var basket = new List<BasketItem>
        {
            new BasketItem(bakedBeans, 2),
            new BasketItem(biscuits, 1)
        };
            var offers = new List<IOffer>();
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(3.18m, result.subTotal);
            Assert.Equal(0m, result.discount);
            Assert.Equal(3.18m, result.total);
        }

        [Fact]
        public void BuyTwoGetOneFree_ShouldApplyDiscount()
        {
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var offer = new SingleProductBuyKGetYFreeOffer(bakedBeans.Name, 2, 1);
            var basket = new List<BasketItem> { new BasketItem(bakedBeans, 3) };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(2.97m, result.subTotal);
            Assert.Equal(0.99m, result.discount);
            Assert.Equal(1.98m, result.total);
        }

        [Fact]
        public void PercentageDiscount_ShouldApplyCorrectly()
        {
            var sardines = new CatalogueItem("Sardines", 1.89m);
            var offer = new PercentageDiscountOffer(new List<string> { sardines.Name }, 25m);
            var basket = new List<BasketItem> { new BasketItem(sardines, 2) };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(3.78m, result.subTotal);
            Assert.Equal(0.94m, result.discount);
            Assert.Equal(2.84m, result.total);
        }

        [Fact]
        public void GroupOffer_ShouldApplyCorrectly()
        {
            var shampooSmall = new CatalogueItem("Shampoo (Small)", 2.00m);
            var shampooMedium = new CatalogueItem("Shampoo (Medium)", 2.50m);
            var shampooLarge = new CatalogueItem("Shampoo (Large)", 3.50m);

            var offer = new GroupBuyKGetCheapestFreeOffer(new List<string> { shampooSmall.Name, shampooMedium.Name, shampooLarge.Name }, 3);
            var basket = new List<BasketItem>
            {
                new BasketItem(shampooLarge, 3),
                new BasketItem(shampooMedium, 1),
                new BasketItem(shampooSmall, 2)
            };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(17.00m, result.subTotal);
            Assert.Equal(5.50m, result.discount);
            Assert.Equal(11.50m, result.total);
        }

        [Fact]
        public void OfferOnProductNotInBasket_ShouldNotApplyDiscount()
        {
            var sardines = new CatalogueItem("Sardines", 1.89m);
            var biscuits = new CatalogueItem("Biscuits", 1.20m);
            var offer = new PercentageDiscountOffer(new List<string> { sardines.Name }, 50m);
            var basket = new List<BasketItem> { new BasketItem(biscuits, 1) };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(1.20m, result.subTotal);
            Assert.Equal(0m, result.discount);
            Assert.Equal(1.20m, result.total);
        }

        [Fact]
        public void MultipleOffersOnSameProduct_ShouldApplyAdditiveDiscounts()
        {
            var sardines = new CatalogueItem("Sardines", 1.89m);
            var offer1 = new PercentageDiscountOffer(new List<string> { sardines.Name }, 25m);
            var offer2 = new PercentageDiscountOffer(new List<string> { sardines.Name }, 25m);
            var basket = new List<BasketItem> { new BasketItem(sardines, 1) };
            var offers = new List<IOffer> { offer1, offer2 };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(1.89m, result.subTotal);
            Assert.Equal(0.94m, result.discount);
            Assert.Equal(0.95m, result.total);
        }

        [Fact]
        public void DiscountExceedsSubTotal_ShouldReturnZeroTotal()
        {
            var sardines = new CatalogueItem("Sardines", 1.89m);
            var offer1 = new PercentageDiscountOffer(new List<string> { sardines.Name }, 100m);
            var offer2 = new PercentageDiscountOffer(new List<string> { sardines.Name }, 100m);
            var basket = new List<BasketItem> { new BasketItem(sardines, 1) };
            var offers = new List<IOffer> { offer1, offer2 };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(1.89m, result.subTotal);
            Assert.Equal(3.78m, result.discount);
            Assert.Equal(0m, result.total);
        }

        [Fact]
        public void BuyTwoGetOneFree_Scaling_ShouldApplyCorrectly()
        {
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var offer = new SingleProductBuyKGetYFreeOffer(bakedBeans.Name, 2, 1);
            var basket = new List<BasketItem> { new BasketItem(bakedBeans, 9) };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(8.91m, result.subTotal);
            Assert.Equal(2.97m, result.discount);
            Assert.Equal(5.94m, result.total);
        }

        [Fact]
        public void BuyTwoGetOneFree_WithRemainder_ShouldApplyCorrectly()
        {
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var offer = new SingleProductBuyKGetYFreeOffer(bakedBeans.Name, 2, 1);
            var basket = new List<BasketItem> { new BasketItem(bakedBeans, 4) };
            var offers = new List<IOffer> { offer };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(3.96m, result.subTotal);
            Assert.Equal(0.99m, result.discount);
            Assert.Equal(2.97m, result.total);
        }

        [Fact]
        public void BasketWithZeroQuantityItem_ShouldIgnore()
        {
            var product = new CatalogueItem("Product A", 10m);
            var basket = new List<BasketItem> { new BasketItem(product, 0) };
            var offers = new List<IOffer>();
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(0m, result.subTotal);
            Assert.Equal(0m, result.discount);
            Assert.Equal(0m, result.total);
        }

        [Fact]
        public void MultipleOffersOnDifferentProducts_ShouldApplyCorrectly()
        {
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var sardines = new CatalogueItem("Sardines", 1.89m);
            var offer1 = new SingleProductBuyKGetYFreeOffer(bakedBeans.Name, 2, 1);
            var offer2 = new PercentageDiscountOffer(new List<string> { sardines.Name }, 25m);
            var basket = new List<BasketItem>
            {
                new BasketItem(bakedBeans, 3),
                new BasketItem(sardines, 2)
            };
            var offers = new List<IOffer> { offer1, offer2 };
            var pricer = new BasketPricer();
            var result = pricer.CalculatePrices(basket, offers);
            Assert.Equal(6.75m, result.subTotal);
            Assert.Equal(1.93m, result.discount);
            Assert.Equal(4.82m, result.total);
        }

        [Fact]
        public void SingleProductBuyKGetYFreeOffer_WithNullItem_ShouldReturnZeroDiscount()
        {
            // Arrange
            var bakedBeans = new CatalogueItem("Baked Beans", 0.99m);
            var offer = new SingleProductBuyKGetYFreeOffer(bakedBeans.Name, 2, 1);
            var basket = new List<BasketItem>
            {
                // Intentionally exclude bakedBeans, include a different product
                new BasketItem(new CatalogueItem("Biscuits", 1.20m), 3)
            };
            var pricer = new BasketPricer();

            // Act
            var result = pricer.CalculatePrices(basket, new List<IOffer> { offer });

            // Assert
            Assert.Equal(3.60m, result.subTotal); // 3 * 1.20m
            Assert.Equal(0m, result.discount);    // No discount as item is null
            Assert.Equal(3.60m, result.total);
        }

        [Fact]
        public void PercentageDiscountOffer_Constructor_WithNegativePercentage_ShouldThrowArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() =>
                new PercentageDiscountOffer(new List<string> { "Example Product" }, -5m));

            // Assert
            Assert.Equal("percentage", exception.ParamName);
            Assert.Contains("Percentage must be between 0 and 100.", exception.Message);
        }

        [Fact]
        public void PercentageDiscountOffer_Constructor_WithPercentageOver100_ShouldThrowArgumentException()
        {
            // Arrange & Act
            var exception = Assert.Throws<ArgumentException>(() =>
                new PercentageDiscountOffer(new List<string> { "Example Product" }, 150m));

            // Assert
            Assert.Equal("percentage", exception.ParamName);
            Assert.Contains("Percentage must be between 0 and 100.", exception.Message);
        }

        [Fact]
        public void PercentageDiscountOffer_CalculateDiscount_WithNoMatchingItems_ShouldReturnZero()
        {
            // Arrange
            var product = new CatalogueItem("Test Product", 1.00m);
            var offer = new PercentageDiscountOffer(new List<string> { "Example Product" }, 10m); // Different GUID
            var basket = new List<BasketItem> { new BasketItem(product, 2) };
            var pricer = new BasketPricer();

            // Act
            var result = pricer.CalculatePrices(basket, new List<IOffer> { offer });

            // Assert
            Assert.Equal(2.00m, result.subTotal); // 2 * 1.00m
            Assert.Equal(0m, result.discount);    // No matching items
            Assert.Equal(2.00m, result.total);
        }
    }
}